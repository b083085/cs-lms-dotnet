using Capstone.LMS.Application.Commands.Book;
using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Queries.Book;
using Capstone.LMS.Domain.Shared;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class BookEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = CreateMapGroup(app, "books")
                .WithTags("Book");

            group.MapGet("{bookId}", GetBookAsync)
                 .WithName("GetBook")
                 .WithSummary("Gets the book details.");

            group.MapPost("list", GetBooksAsync)
                 .WithName("GetBooks")
                 .WithSummary("Gets a list of books.");

            group.MapPost("borrowed", GetBorrowedBooksAsync)
                 .WithName("GetBorrowedBooks")
                 .WithSummary("Gets a list of borrowed books.");

            group.MapGet("overdue", GetOverdueBooksAsync)
                 .WithName("GetOverdueBooks")
                 .WithSummary("Gets a list of overdue books.");

            group.MapGet("popular", GetPopularBooksAsync)
                 .WithName("GetPopularBooks")
                 .WithSummary("Gets a list of popular books.");

            group.MapPost("borrow", BorrowBookAsync)
                 .WithName("BorrowBook")
                 .WithSummary("Borrows a book.");

            group.MapPost("", CreateBookAsync)
                 .WithName("CreateBook")
                 .WithSummary("Creates a book.");

            group.MapDelete("{bookId}", DeleteBookAsync)
                 .WithName("DeleteBook")
                 .WithSummary("Deletes a book.");

            group.MapPost("return", ReturnBookAsync)
                 .WithName("ReturnBook")
                 .WithSummary("Returns the book.");

            group.MapPut("", UpdateBookAsync)
                 .WithName("UpdateBook")
                 .WithSummary("Updates the book.");
        }

        private static async Task<Results<Ok<GetBookResponseDto>, NotFound<Error>>> GetBookAsync(
            IMediator mediator,
            Guid bookId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetBookQuery(bookId), cancellationToken);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result.Value);
            }

            return TypedResults.NotFound(result.Error);
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
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return TypedResults.Created("", result.Value);
            }

            return TypedResults.Conflict(result.Error);
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
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result.Value);
            }

            return TypedResults.BadRequest(result.Error);
        }
    }
}
