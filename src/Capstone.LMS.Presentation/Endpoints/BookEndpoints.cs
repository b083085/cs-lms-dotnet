using Capstone.LMS.Application.Commands.Book;
using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Queries.Book;
using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Shared;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class BookEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var book = CreateMapGroup(app, "books")
                .WithTags("Book");

            book.MapGet("{bookId}", GetBookAsync)
                 .WithName(EndpointNames.Book.GetBook)
                 .WithSummary("Gets the book details.");

            book.MapPost("list", GetBooksAsync)
                 .WithSummary("Gets the list of books.");

            book.MapPost("borrowed", GetBorrowedBooksAsync)
                 .WithSummary("Gets the list of borrowed books.");

            book.MapGet("overdue", GetOverdueBooksAsync)
                 .WithSummary("Gets the list of overdue books.");

            book.MapGet("popular", GetPopularBooksAsync)
                 .WithSummary("Gets the list of popular books.");

            book.MapPost("borrow", BorrowBookAsync)
                 .WithSummary("Borrows the book.");

            book.MapPost("", CreateBookAsync)
                 .WithSummary("Creates a book.");

            book.MapDelete("{bookId}", DeleteBookAsync)
                 .WithSummary("Deletes the book.");

            book.MapPost("return", ReturnBookAsync)
                 .WithSummary("Returns the book.");

            book.MapPut("", UpdateBookAsync)
                 .WithSummary("Updates the book.");
        }

        private static async Task<Results<Ok<GetBookResponseDto>, NotFound<Error>>> GetBookAsync(
            IMediator mediator,
            Guid bookId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetBookQuery(bookId), cancellationToken);
            
            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.NotFound(result.Error);
        }

        private static async Task<Ok<ListResponseDto<GetBookResponseDto>>> GetBooksAsync(
            IMediator mediator,
            GetBooksQuery command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<ListResponseDto<GetBorrowedBookResponseDto>>> GetBorrowedBooksAsync(
            IMediator mediator,
            GetBorrowedBooksQuery command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<IEnumerable<GetOverdueBookResponseDto>>> GetOverdueBooksAsync(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetOverdueBooksQuery(), cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<IEnumerable<GetPopularBookResponseDto>>> GetPopularBooksAsync(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetPopularBooksQuery(), cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<SuccessResponseDto>> BorrowBookAsync(
            IMediator mediator,
            BorrowBookCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Created<CreateBookResponseDto>, Conflict<Error>>> CreateBookAsync(
            IMediator mediator,
            CreateBookCommand command,
            LinkGenerator links,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            
            return result.IsSuccess ?
                TypedResults.Created(links.GetPathByName(EndpointNames.Book.GetBook, new { bookId = result.Value.BookId }), result.Value) :
                TypedResults.Conflict(result.Error);
        }

        private static async Task<Ok<SuccessResponseDto>> DeleteBookAsync(
            IMediator mediator,
            Guid bookId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteBookCommand(bookId), cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<SuccessResponseDto>> ReturnBookAsync(
            IMediator mediator,
            ReturnBookCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok<UpdateBookResponseDto>, BadRequest<Error>>> UpdateBookAsync(
            IMediator mediator,
            UpdateBookCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.BadRequest(result.Error);
        }
    }
}
