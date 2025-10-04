using Capstone.LMS.Application.Commands.Book;
using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Queries.Book;
using Capstone.LMS.Domain.Shared;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class ReturnBookEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var book = CreateMapGroup(app, "return-books")
                .WithTags("ReturnBooks");

            //book.MapGet("", GetBooksReturnedAsync)
            //     .WithSummary("Gets the list of books returned.");

            //book.MapGet("{userId:guid}", GetBooksReturnedByUserAsync)
            //     .WithSummary("Gets the list of books returned by the user.");

            //book.MapPut("", UpdateBookAsync)
            //     .WithSummary("Returns the book.");
        }

        //private static async Task<Results<Ok<GetBookResponseDto>, NotFound<Error>>> GetBooksReturnedAsync(
        //    IMediator mediator,
        //    Guid bookId,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await mediator.Send(new GetBookQuery(bookId), cancellationToken);
            
        //    return result.IsSuccess ?
        //        TypedResults.Ok(result.Value) :
        //        TypedResults.NotFound(result.Error);
        //}

        //private static async Task<Ok<ListResponseDto<GetBookResponseDto>>> GetBooksReturnedByUserAsync(
        //    IMediator mediator,
        //    GetBooksQuery command,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await mediator.Send(command, cancellationToken);

        //    return TypedResults.Ok(result);
        //}

        //private static async Task<Results<Ok<UpdateBookResponseDto>, BadRequest<Error>>> UpdateBookAsync(
        //    IMediator mediator,
        //    UpdateBookCommand command,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await mediator.Send(command, cancellationToken);

        //    return result.IsSuccess ?
        //        TypedResults.Ok(result.Value) :
        //        TypedResults.BadRequest(result.Error);
        //}
    }
}
