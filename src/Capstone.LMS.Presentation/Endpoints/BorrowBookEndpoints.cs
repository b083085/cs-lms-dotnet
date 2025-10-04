using Capstone.LMS.Application.Commands.Book;
using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Queries.Book;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class BorrowBookEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var book = CreateMapGroup(app, "borrow-books")
                .WithTags("BorrowBooks");

            //book.MapGet("", GetBooksBorrowedAsync)
            //     .WithSummary("Gets the list of books borrowed.");

            //book.MapGet("{userId:guid}", GetBooksBorrowedByUserAsync)
            //     .WithSummary("Gets the list of books borrowed by the user.");

            //book.MapPost("", BorrowBookAsync)
            //     .WithSummary("Borrows the book.");
        }

        //private static async Task<Ok<ListResponseDto<GetBorrowedBookResponseDto>>> GetBooksBorrowedAsync(
        //    IMediator mediator,
        //    string searchTerm,
        //    string sortBy,
        //    string sortDirection,
        //    int page,
        //    int pageSize,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await mediator.Send(command, cancellationToken);

        //    return TypedResults.Ok(result);
        //}

        //private static async Task<Ok<ListResponseDto<GetBorrowedBookResponseDto>>> GetBooksBorrowedByUserAsync(
        //    IMediator mediator,
        //    GetBorrowedBooksQuery command,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await mediator.Send(command, cancellationToken);

        //    return TypedResults.Ok(result);
        //}

        //private static async Task<Ok<SuccessResponseDto>> BorrowBookAsync(
        //    IMediator mediator,
        //    BorrowBookCommand command,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await mediator.Send(command, cancellationToken);

        //    return TypedResults.Ok(result);
        //}
    }
}
