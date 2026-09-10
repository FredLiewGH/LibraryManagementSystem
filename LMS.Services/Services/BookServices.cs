using LMS.EFCore.Abstractions;
using LMS.EFCore.Entities;
using LMS.Services.Abstractions;
using LMS.Services.Contracts.Constants;
using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.DTOs.Books;
using LMS.Services.Contracts.Enums;
using Microsoft.Extensions.Logging;

namespace LMS.Services.Services
{
    public sealed class BookServices : ServiceExecution, IBookServices
    {
        private readonly ICRUD<Tblbooks> _crudBooks;
        private readonly ICRUD<Tblborrows> _crudBorrows;
        private readonly IDBCommit _commiter;

        public BookServices(
            ICRUD<Tblbooks> crudBooks,
            ICRUD<Tblborrows> crudBorrows,
            IDBCommit commiter,
            IAPIResponseBuilder apiResBuilder, ILogger<BookServices> logger) : base(apiResBuilder, logger)
        {
            _crudBooks = crudBooks ?? throw new ArgumentNullException(nameof(crudBooks));
            _crudBorrows = crudBorrows ?? throw new ArgumentNullException(nameof(crudBorrows));
            _commiter = commiter ?? throw new ArgumentNullException(nameof(commiter));
        }

        public Task<APIResponse> GetAll(CancellationToken token = default) => Execute(async () =>
        {
            IReadOnlyList<Tblbooks> books = await _crudBooks.GetAllAsync(token);

            List<BookResponse> result = books.Select(b => new BookResponse
            {
                BookId = b.BookID,
                BookName = b.BookName,
                Author = b.Author,
                Publisher = b.Publisher,
                Status = b.Status
            }).ToList();

            return _apiResBuilder.Success(null, result);
        });

        public Task<APIResponse> GetById(Guid id, CancellationToken token = default) => Execute(async () =>
        {
            Tblbooks? existing_book = await _crudBooks.GetByIdAsync(id, token);
            if (existing_book == null)
            {
                return _apiResBuilder.NotFound();
            }

            BookResponse result = new BookResponse
            {
                BookId = existing_book.BookID,
                BookName = existing_book.BookName,
                Author = existing_book.Author,
                Publisher = existing_book.Publisher,
                Status = existing_book.Status
            };

            return _apiResBuilder.Success(null, result);
        });

        public Task<APIResponse> Create(CreateBookRequest request, CancellationToken token = default) => Execute(async () =>
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));            
            Tblbooks newBook = new Tblbooks
            {
                BookID = Guid.NewGuid(),
                BookName = request.BookName,
                Author = request.Author,
                Publisher = request.Publisher,
                Status = BOOK_STATUS.AVAILABLE.ToString(),
                CreatedTimestamp = DateTime.UtcNow,
            };

            await _crudBooks.AddAsync(newBook, token);
            await _commiter.SaveChangesAsync(token);

            return _apiResBuilder.Success(Messages.CreateSuccessMessage);
        });

        public Task<APIResponse> Update(Guid id, UpdateBookRequest request, CancellationToken token = default) => Execute(async () =>
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            // Check if record exists
            Tblbooks? existing_book = await _crudBooks.GetByIdAsync(id, token);
            if (existing_book == null)
            {
                return _apiResBuilder.NotFound();
            }

            existing_book.BookName = request.BookName ?? existing_book.BookName;
            existing_book.Author = request.Author ?? existing_book.Author;
            existing_book.Publisher = request.Publisher ?? existing_book.Publisher;

            await _crudBooks.UpdateAsync(existing_book, token);
            await _commiter.SaveChangesAsync(token);

            return _apiResBuilder.Success(Messages.UpdateSuccessMessage);
        });

        public Task<APIResponse> Delete(Guid id, CancellationToken token = default) => Execute(async () =>
        {
            // Check if record exists
            Tblbooks? existing_book = await _crudBooks.GetByIdAsync(id, token);
            if (existing_book == null)
            {
                return _apiResBuilder.NotFound();
            }

            // Check if book currently borrowed
            IReadOnlyList<Tblborrows> history = await _crudBorrows.FindAsync(b => b.BookID == id, token);
            if (history.Count > 0)
            {
                return _apiResBuilder.Conflict(existing_book.Status == BOOK_STATUS.BORROWED.ToString()
                    ? "Cannot delete a book that is currently borrowed."
                    : "Cannot delete a book that has borrow history.");
            }

            await _crudBooks.DeleteAsync(existing_book, token);
            await _commiter.SaveChangesAsync(token);

            return _apiResBuilder.Success(Messages.DeleteSuccessMessage);
        });
    }
}
