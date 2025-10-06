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
    public sealed class GetBooksQueryHandler(
        IBookRepository bookRepository,
        ILogger<GetBooksQueryHandler> logger) 
        : IRequestHandler<GetBooksQuery, ListResponseDto<GetBookItemResponseDto>>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly ILogger<GetBooksQueryHandler> _logger = logger;

        public async Task<ListResponseDto<GetBookItemResponseDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            // get queryable
            var query = _bookRepository.GetQueryable();

            // includes
            query = query
                .Include(q => q.Author)
                .Include(q => q.Genre)
                .Include(q => q.BorrowedBooks);

            // filter
            var searchTermLowerCase = request.SearchTerm?.ToLower();

            query = request.PublishedYear is null || request.PublishedYear <= 0 ? query : query.Where(f => f.PublishedOn.Year == request.PublishedYear.Value);
            query = request.Author.IsEmpty() ? query : query.Where(f => f.Author.Name == request.Author);
            query = request.Availability is null ? query : query.Where(f => f.Availability == request.Availability);
            query = request.SearchTerm.IsEmpty() ? query :
                    query.Where(f =>
                        f.Title.ToLower().Contains(searchTermLowerCase) ||
                        f.Summary.ToLower().Contains(searchTermLowerCase) ||
                        f.Author.Name.ToLower().Contains(searchTermLowerCase) ||
                        f.Genre.Name.ToLower().Contains(searchTermLowerCase) ||
                        f.Isbn.ToLower().Contains(searchTermLowerCase));

            // get total count
            var total = await query.CountAsync(cancellationToken);

            // sort
            if (request.SortBy.IsNotEmpty())
            {
                Expression<Func<Domain.Entities.Book, object>> GetSortBy()
                {
                    return request.SortBy.ToLower() switch
                    {
                        "id"            => p => p.Id,
                        "title"         => p => p.Title,
                        "summary"       => p => p.Summary,
                        "isbn"          => p => p.Isbn,
                        "availability"  => p => p.Availability,
                        "author"        => p => p.Author.Name,
                        "genre"         => p => p.Genre.Name,
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
                .Select(book => new GetBookItemResponseDto
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Summary = book.Summary,
                    Isbn = book.Isbn,
                    PublishedOn = book.PublishedOn,
                    TotalCopies = book.TotalCopies,
                    Availability = book.Availability,
                    Genre = book.Genre == null ? null : new Dtos.Genre.GetGenreResponseDto
                    {
                        GenreId = book.Genre.Id,
                        Name = book.Genre.Name
                    },
                    Author = book.Author == null ? null : new Dtos.Author.GetAuthorResponseDto
                    {
                        AuthorId = book.Author.Id,
                        Name = book.Author.Name
                    }
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);


            var result = new ListResponseDto<GetBookItemResponseDto>
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
