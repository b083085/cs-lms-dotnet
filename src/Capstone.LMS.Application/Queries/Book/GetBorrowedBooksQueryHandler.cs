using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Extensions;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetBorrowedBooksQueryHandler(
        IBorrowedBookRepository borrowedBookRepository,
        ILogger<GetBorrowedBooksQueryHandler> logger) : IRequestHandler<GetBorrowedBooksQuery, ListResponseDto<GetBookBorrowedResponseDto>>
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
        private readonly ILogger<GetBorrowedBooksQueryHandler> _logger = logger;

        public async Task<ListResponseDto<GetBookBorrowedResponseDto>> Handle(GetBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            // get queryable
            var query = _borrowedBookRepository.GetQueryable();

            // includes
            query = query
                .Include(q => q.Book).ThenInclude(q => q.Genre)
                .Include(q => q.Book).ThenInclude(q => q.Author)
                .Include(q => q.User)
                .Include(q => q.Approver);

            // filter
            var searchTermLowerCase = request.SearchTerm?.ToLower();

            query = request.SearchTerm.IsEmpty() ? query :
                    query.Where(f =>
                        f.Book.Title.ToLower().Contains(searchTermLowerCase) ||
                        f.Book.Summary.ToLower().Contains(searchTermLowerCase) ||
                        f.Book.Author.Name.ToLower().Contains(searchTermLowerCase) ||
                        f.Book.Genre.Name.ToLower().Contains(searchTermLowerCase) ||
                        f.Book.Isbn.ToLower().Contains(searchTermLowerCase));

            query = query.Where(p => p.Status == Domain.Enums.BorrowedStatus.Borrowed);

            // get total count
            var total = await query.CountAsync(cancellationToken);

            // sort
            if (request.SortBy.IsNotEmpty())
            {
                Expression<Func<Domain.Entities.BorrowedBook, object>> GetSortBy()
                {
                    return request.SortBy.ToLower() switch
                    {
                        "id"            => p => p.Id,
                        "title"         => p => p.Book.Title,
                        "summary"       => p => p.Book.Summary,
                        "isbn"          => p => p.Book.Isbn,
                        "availability"  => p => p.Book.Availability,
                        "author"        => p => p.Book.Author.Name,
                        "genre"         => p => p.Book.Genre.Name,
                        _               => null
                    };
                }

                var sortBy = GetSortBy();

                if (sortBy is not null)
                {
                    query = request.SortDirection == Domain.Enums.SortDirection.Descending ?
                        query.OrderByDescending(sortBy) :
                        query.OrderBy(sortBy);
                }
            }

            // pagination
            var skip = (request.Page.Value - 1) * request.PageSize.Value;
            var take = request.PageSize.Value;

            query = query
                .Skip(skip)
                .Take(take);

            // retrieve
            var items = await query
                .Select(bookBorrowed => new GetBookBorrowedResponseDto
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
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);


            var result = new ListResponseDto<GetBookBorrowedResponseDto>
            {
                Page = request.Page.Value,
                PageSize = request.PageSize.Value,
                Total = total,
                Items = items
            };

            return result;
        }
    }
}
