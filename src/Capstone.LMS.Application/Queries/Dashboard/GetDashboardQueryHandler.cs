using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Dtos.Dashboard;
using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Dashboard
{
    public sealed class GetDashboardQueryHandler(
        IBorrowedBookRepository borrowedBookRepository) : 
        IRequestHandler<GetDashboardQuery, GetDashboardResponseDto>
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;

        public async Task<GetDashboardResponseDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var dashboard = new GetDashboardResponseDto();

            var borrowedBookQuery = _borrowedBookRepository.GetQueryable();

            borrowedBookQuery = borrowedBookQuery
                            .Include(p => p.Book).ThenInclude(p => p.Genre)
                            .Include(p => p.Book).ThenInclude(p => p.Author)
                            .Include(p => p.Approver);


            switch (request.Role)
            {
                case Roles.Administrator:
                    {

                    }
                    break;
                case Roles.Librarian:
                    {
                        var borrowedBooks = await borrowedBookQuery
                            .Where(p => p.Status == Domain.Enums.BorrowedStatus.Borrowed)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

                        var dueBooks = await borrowedBookQuery
                            .Where(p => p.Status == Domain.Enums.BorrowedStatus.Overdue)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

                        var topBorrowers = await borrowedBookQuery
                            .GroupBy(p => new { p.UserId, p.User.FirstName, p.User.LastName })
                            .Select(p => new
                            {
                                User = p.Key,
                                Total = p.Count()
                            })
                            .OrderByDescending(p => p.Total)
                            .Take(5)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

                        var topBooks = borrowedBooks
                            .GroupBy(p => new { p.BookId, p.Book.Title, p.Book.Summary })
                            .Select(p => new
                            {
                                Book = p.Key,
                                Total = p.Count()
                            })
                            .OrderByDescending(p => p.Total)
                            .Take(5)
                            .ToList();

                        dashboard.Tables =
                        [
                            .. CreateDashboardTables(borrowedBooks, dueBooks),
                            new(
                            "Top 5 Borrowers",
                            new List<string>{ "Borrower" },
                            topBorrowers.Select(b => b.User.FirstName + " " + b.User.LastName)),

                            new(
                            "Top 5 Books",
                            new List<string>{ "Book" },
                            topBooks.Select(b => b.Book.Title))
                        ];
                    }
                    break;
                case Roles.Borrower:
                    {
                        var borrowedBooks = await borrowedBookQuery
                            .Where(p => p.UserId == request.UserId && p.Status == Domain.Enums.BorrowedStatus.Borrowed)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

                        var dueBooks = await borrowedBookQuery
                            .Where(p => p.UserId == request.UserId && p.Status == Domain.Enums.BorrowedStatus.Overdue)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

                        dashboard.Tables = CreateDashboardTables(borrowedBooks, dueBooks);
                    }
                    break;
            }

            return dashboard;
        }

        private IEnumerable<DashboardTableDto> CreateDashboardTables(List<BorrowedBook> borrowedBooks, List<BorrowedBook> overdueBooks)
        {
            yield return new(
                    "Books Borrowed",
                    new List<string>
                    {
                                    nameof(DashboardBorrowedBookItemDto.Title),
                                    nameof(DashboardBorrowedBookItemDto.Summary),
                                    nameof(DashboardBorrowedBookItemDto.Issued),
                                    nameof(DashboardBorrowedBookItemDto.Due)
                    },
                    borrowedBooks
                    .Select(b => new DashboardBorrowedBookItemDto
                    {
                        Title = b.Book.Title,
                        Summary = b.Book.Summary,
                        Issued = b.BorrowedOnUtc,
                        Due = b.DueOnUtc
                    }));

            yield return new(
                    "Books Overdue",
                    new List<string>
                    {
                                    nameof(DashboardOverdueBookItemDto.Title),
                                    nameof(DashboardOverdueBookItemDto.Summary),
                                    nameof(DashboardOverdueBookItemDto.Issued),
                                    nameof(DashboardOverdueBookItemDto.Due),
                                    nameof(DashboardOverdueBookItemDto.ElapsedInDays),
                    },
                    overdueBooks
                    .Select(b => new DashboardOverdueBookItemDto
                    {
                        Title = b.Book.Title,
                        Summary = b.Book.Summary,
                        Issued = b.BorrowedOnUtc,
                        Due = b.DueOnUtc,
                        ElapsedInDays = b.DueOnUtc is null ? 0 : (DateTime.UtcNow - b.DueOnUtc).Value.Days
                    }));
        }

        private static GetBookBorrowedResponseDto SetBookBorrowedModel(BorrowedBook bookBorrowed)
        {
            return new GetBookBorrowedResponseDto
            {
                BookBorrowedId = bookBorrowed.Id,
                Status = bookBorrowed.Status,
                ApproverName = bookBorrowed.Approver == null ? "" : bookBorrowed.Approver.FirstName + " " + bookBorrowed.Approver.LastName,
                ApprovedOnUtc = bookBorrowed.ApprovedOnUtc,
                BorrowedOnUtc = bookBorrowed.BorrowedOnUtc,
                DueOnUtc = bookBorrowed.DueOnUtc,
                ReturnedOnUtc = bookBorrowed.ReturnedOnUtc,
                Book = new GetBookItemResponseDto
                {
                    BookId = bookBorrowed.Book.Id,
                    Title = bookBorrowed.Book.Title,
                    Summary = bookBorrowed.Book.Summary,
                    Isbn = bookBorrowed.Book.Isbn,
                    PublishedOn = bookBorrowed.Book.PublishedOn,
                    TotalCopies = bookBorrowed.Book.TotalCopies,
                    Availability = bookBorrowed.Book.Availability,
                    Genre = bookBorrowed.Book.Genre == null ? null : new Dtos.Genre.GetGenreResponseDto
                    {
                        GenreId = bookBorrowed.Book.Genre.Id,
                        Name = bookBorrowed.Book.Genre.Name
                    },
                    Author = bookBorrowed.Book.Author == null ? null : new Dtos.Author.GetAuthorResponseDto
                    {
                        AuthorId = bookBorrowed.Book.Author.Id,
                        Name = bookBorrowed.Book.Author.Name
                    }
                }
            };
        }
    }
}
