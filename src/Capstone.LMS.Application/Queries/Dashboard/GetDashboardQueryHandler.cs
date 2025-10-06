using Capstone.LMS.Application.Dtos.Dashboard;
using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Enums;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Dashboard
{
    public sealed class GetDashboardQueryHandler(
        IBorrowedBookRepository borrowedBookRepository,
        UserManager<Domain.Entities.User> userManager) :
        IRequestHandler<GetDashboardQuery, GetDashboardResponseDto>
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
        private readonly UserManager<Domain.Entities.User> _userManager = userManager;

        public async Task<GetDashboardResponseDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var dashboard = new GetDashboardResponseDto();

            var borrowedBookQuery = _borrowedBookRepository.GetQueryable();

            borrowedBookQuery = borrowedBookQuery
                            .Include(p => p.Book).ThenInclude(p => p.Genre)
                            .Include(p => p.Book).ThenInclude(p => p.Author)
                            .Include(p => p.User)
                            .Include(p => p.Approver);


            switch (request.Role)
            {
                case Roles.Administrator:
                    {
                        var borrowedOrOverdueBooks = await borrowedBookQuery
                            .Where(p => p.Status == BorrowedStatus.Borrowed || p.Status == BorrowedStatus.Overdue)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

                        var borrowedBooks = borrowedOrOverdueBooks
                            .Where(p => p.Status == BorrowedStatus.Borrowed)
                            .ToList();

                        var librarians = await _userManager.GetUsersInRoleAsync(Roles.Librarian);
                        var borrowers = await _userManager.GetUsersInRoleAsync(Roles.Borrower);

                        dashboard.Cards =
                        [
                            new("Total Librarians", librarians.Count),
                            new("Total Borrowers", borrowers.Count),
                            new("Borrowed Books", borrowedBooks.Count)
                        ];

                        dashboard.List =
                        [
                            CreateTopBorrowersList(borrowedOrOverdueBooks),
                            CreateTopBooksList(borrowedOrOverdueBooks)
                        ];
                    }
                    break;
                case Roles.Librarian:
                    {
                        var borrowedOrOverdueBooks = await borrowedBookQuery
                            .Where(p => p.UserId == request.UserId && (p.Status == BorrowedStatus.Borrowed || p.Status == BorrowedStatus.Overdue))
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

                        dashboard.Tables =
                        [
                            CreateDashboardBorrowedBooksTable(borrowedOrOverdueBooks.Where(b => b.Status == BorrowedStatus.Borrowed)),
                            CreateDashboardOverdueBooksTable(borrowedOrOverdueBooks.Where(b => b.Status == BorrowedStatus.Overdue))
                        ];

                        dashboard.List =
                        [
                            CreateTopBorrowersList(borrowedOrOverdueBooks),
                            CreateTopBooksList(borrowedOrOverdueBooks)
                        ];
                    }
                    break;
                case Roles.Borrower:
                    {
                        var borrowedOrOverdueBooks = await borrowedBookQuery
                            .Where(p => p.UserId == request.UserId && (p.Status == BorrowedStatus.Borrowed || p.Status == BorrowedStatus.Overdue))
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

                        dashboard.Tables =
                        [
                            CreateDashboardBorrowedBooksTable(borrowedOrOverdueBooks.Where(b => b.Status == BorrowedStatus.Borrowed)),
                            CreateDashboardOverdueBooksTable(borrowedOrOverdueBooks.Where(b => b.Status == BorrowedStatus.Overdue))
                        ];
                    }
                    break;
            }

            return dashboard;
        }

        private static DashboardListDto CreateTopBorrowersList(IEnumerable<BorrowedBook> borrowedOrOverdueBooks)
        {
            var topBorrowers = borrowedOrOverdueBooks
                            .GroupBy(p => new
                            {
                                p.UserId,
                                p.User.FirstName,
                                p.User.LastName
                            })
                            .Select(p => new
                            {
                                User = p.Key,
                                Total = p.Count()
                            })
                            .OrderByDescending(p => p.Total)
                            .Take(5)
                            .ToList();

            return new("Top 5 Borrowers", topBorrowers.Select(b => $"{b.User.FirstName} {b.User.LastName}"));
        }

        private static DashboardListDto CreateTopBooksList(IEnumerable<BorrowedBook> borrowedOrOverdueBooks)
        {
            var topBooks = borrowedOrOverdueBooks
                            .GroupBy(p => new
                            {
                                p.BookId,
                                p.Book.Title,
                                p.Book.Summary
                            })
                            .Select(p => new
                            {
                                Book = p.Key,
                                Total = p.Count()
                            })
                            .OrderByDescending(p => p.Total)
                            .Take(5)
                            .ToList();

            return new("Top 5 Books", topBooks.Select(b => b.Book.Title));
        }

        private static DashboardTableDto CreateDashboardBorrowedBooksTable(IEnumerable<BorrowedBook> borrowedBooks)
        {
            return new(
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
        }

        private static DashboardTableDto CreateDashboardOverdueBooksTable(IEnumerable<BorrowedBook> overdueBooks)
        {
            return new(
                    "Overdue Books",
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

    }
}
