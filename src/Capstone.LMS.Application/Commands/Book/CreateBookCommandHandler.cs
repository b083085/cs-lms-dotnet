using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class CreateBookCommandHandler(
        IBookRepository bookRepository,
        IGenreRepository genreRepository,
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateBookCommandHandler> logger)
        : IRequestHandler<CreateBookCommand, Result<CreateBookResponseDto>>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateBookCommandHandler> _logger = logger;

        public async Task<Result<CreateBookResponseDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(b => b.Title.ToLower() == request.Title.ToLower(), cancellationToken);
            if (book is not null)
            {
                return Result.Failure<CreateBookResponseDto>(DomainErrors.Book.AlreadyExist);
            }

            var genre = await _genreRepository.GetAsync(g => g.Id == request.GenreId, cancellationToken);
            if (genre is null)
            {
                return Result.Failure<CreateBookResponseDto>(DomainErrors.Genre.NotFound);
            }

            var author = await _authorRepository.GetAsync(a => a.Id ==  request.AuthorId, cancellationToken);   
            if(author is null)
            {
                return Result.Failure<CreateBookResponseDto>(DomainErrors.Author.NotFound);
            }

            book = Domain.Entities.Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Summary,
                request.Isbn,
                request.PublishedOn,
                request.TotalCopies,
                genre,
                author);

            await _bookRepository.CreateAsync(book, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Book is created. {BookTitle}", book.Title);

            return new CreateBookResponseDto
            {
                BookId = book.Id,
                Title = book.Title,
                Summary = book.Summary,
                Isbn = book.Isbn,
                PublishedOn = book.PublishedOn,
                TotalCopies = book.TotalCopies,
                Availability = book.Availability,
                Genre = new Dtos.Genre.GetGenreResponseDto
                {
                    GenreId = genre.Id,
                    Name = genre.Name
                },
                Author = new Dtos.Author.GetAuthorResponseDto
                {
                    AuthorId = author.Id,
                    Name = author.Name
                }
            };
        }
    }
}
