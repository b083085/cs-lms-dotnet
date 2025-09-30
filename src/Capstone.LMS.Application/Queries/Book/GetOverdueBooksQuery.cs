using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Collections.Generic;

namespace Capstone.LMS.Application.Queries.Book
{
    public record GetOverdueBooksQuery() : IRequest<IEnumerable<GetOverdueBookResponseDto>>;
}
