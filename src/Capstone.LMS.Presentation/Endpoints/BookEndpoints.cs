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

            book.MapGet("{bookId:guid}", GetBookAsync)
                 .WithName(EndpointNames.Book.GetBook)
                 .WithSummary("Gets the book details.");

            book.MapGet("", GetBooksAsync)
                 .WithSummary("Gets the list of books.");

            book.MapPost("", CreateBookAsync)
                 .WithSummary("Creates a book.");

            book.MapDelete("{bookId}", DeleteBookAsync)
                 .WithSummary("Deletes the book.");

            book.MapPut("", UpdateBookAsync)
                 .WithSummary("Updates the book.");

            book.MapGet("borrow/{bookBorrowedId:guid}", GetBookBorrowedAsync)
                 .WithName(EndpointNames.Book.GetBookBorrowed)
                 .WithSummary("Gets the details of the book borrowed.");

            book.MapPost("borrow/request", RequestBorrowBookAsync)
                 .WithSummary("Request to borrow the book.");

            book.MapPut("borrow/approve", ApproveRequestBorrowBookAsync)
                 .WithSummary("Approves the request to borrow the book.");

            book.MapPut("borrow/return/{bookBorrowedId:guid}", ReturnBorrowedBookBookAsync)
                 .WithSummary("Returns the borrowed book.");

            book.MapGet("popular", GetPopularBooksAsync)
                 .WithSummary("Gets the list of popular books.");

            book.MapGet("overdue", GetOverdueBooksAsync)
                 .WithSummary("Gets the list of overdue books.");

            book.MapGet("borrowed", GetBorrowedBooksAsync)
                 .WithSummary("Gets the list of borrowed books.");

            book.MapGet("returned", GetReturnedBooksAsync)
                 .WithSummary("Gets the list of returned books.");

            book.MapGet("borrowed/by/user", GetUserBorrowedBooksAsync)
                 .WithSummary("Gets the list of user borrowed books.");

            book.MapGet("returned/by/user", GetUserReturnedBooksAsync)
                 .WithSummary("Gets the list of user returned books.");
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

        private static async Task<Ok<ListResponseDto<GetBookItemResponseDto>>> GetBooksAsync(
            IMediator mediator,
            [AsParameters]GetBooksQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

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

        private static async Task<Results<Ok, BadRequest<Error>>> DeleteBookAsync(
            IMediator mediator,
            Guid bookId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteBookCommand(bookId), cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.BadRequest(result.Error);
        }

        private static async Task<Results<Ok, BadRequest<Error>>> UpdateBookAsync(
            IMediator mediator,
            UpdateBookCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.BadRequest(result.Error);
        }

        private static async Task<Results<Ok<GetBookBorrowedResponseDto>, NotFound<Error>>> GetBookBorrowedAsync(
            IMediator mediator,
            Guid bookBorrowedId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetBookBorrowedQuery(bookBorrowedId), cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.NotFound(result.Error);
        }

        private static async Task<Results<Created<BorrowBookResponseDto>, Conflict<Error>>> RequestBorrowBookAsync(
            IMediator mediator,
            RequestBorrowBookCommand command,
            LinkGenerator links,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Created(links.GetPathByName(EndpointNames.Book.GetBookBorrowed, new { bookBorrowedId = result.Value.BookBorrowedId }), result.Value) :
                TypedResults.Conflict(result.Error);
        }

        private static async Task<Results<Ok, Conflict<Error>>> ApproveRequestBorrowBookAsync(
            IMediator mediator,
            ApproveBorrowBookCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.Conflict(result.Error);
        }

        private static async Task<Results<Ok, Conflict<Error>>> ReturnBorrowedBookBookAsync(
            IMediator mediator,
            [AsParameters]ReturnBookCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.Conflict(result.Error);
        }

        private static async Task<Ok<IEnumerable<GetPopularBookResponseDto>>> GetPopularBooksAsync(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetPopularBooksQuery(), cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<IEnumerable<GetOverdueBookResponseDto>>> GetOverdueBooksAsync(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetOverdueBooksQuery(), cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<ListResponseDto<GetBookBorrowedResponseDto>>> GetBorrowedBooksAsync(
            IMediator mediator,
            [AsParameters]GetBorrowedBooksQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<ListResponseDto<GetBookBorrowedResponseDto>>> GetReturnedBooksAsync(
            IMediator mediator,
            [AsParameters] GetReturnedBooksQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<ListResponseDto<GetBookBorrowedResponseDto>>> GetUserBorrowedBooksAsync(
            IMediator mediator,
            [AsParameters] GetUserBorrowedBooksQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Ok<ListResponseDto<GetBookBorrowedResponseDto>>> GetUserReturnedBooksAsync(
            IMediator mediator,
            [AsParameters] GetUserReturnedBooksQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return TypedResults.Ok(result);
        }
    }
}
