using LMS.EFCore.Abstractions;
using LMS.EFCore.Entities;
using LMS.Services.Abstractions;
using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.Enums;
using Microsoft.Extensions.Logging;

namespace LMS.Services.Services
{
    public sealed class BorrowServices : ServiceExecution, IBorrowServices
    {
        private readonly ICRUD<Tblborrows> _crudBorrows;
        private readonly ICRUD<Tblbooks> _crudBooks;
        private readonly ICRUD<Tblmembers> _crudMembers;
        private readonly IDBCommit _commiter;

        public BorrowServices(
            ICRUD<Tblborrows> crudBorrows,
            ICRUD<Tblbooks> crudBooks,
            ICRUD<Tblmembers> crudMembers,
            IDBCommit commiter,
            IAPIResponseBuilder apiResBuilder, ILogger<BorrowServices> logger) : base(apiResBuilder, logger)
        {
            _crudBorrows = crudBorrows ?? throw new ArgumentNullException(nameof(crudBorrows));
            _crudBooks = crudBooks ?? throw new ArgumentNullException(nameof(crudBooks));
            _crudMembers = crudMembers ?? throw new ArgumentNullException(nameof(crudMembers));
            _commiter = commiter ?? throw new ArgumentNullException(nameof(commiter));            
        }

        public Task<APIResponse> BorrowBook(Borrows borrow, CancellationToken token = default) => Execute(async () =>
        {
            if (borrow.BorrowEndDate < borrow.BorrowStartDate) 
            {
                return _apiResBuilder.Fail("End date cannot be earlier than start date!");
            }

            Tblmembers? existing_member = await _crudMembers.GetByIdAsync(borrow.MemberId, token);
            if (existing_member == null)
            {
                return _apiResBuilder.NotFound("Member not found!");
            }

            Tblbooks? existing_book = await _crudBooks.GetByIdAsync(borrow.BookId, token);
            if (existing_book == null)
            {
                return _apiResBuilder.NotFound("Book not found!");
            }

            // Check if the book is already borrowed
            if (existing_book.Status == BOOK_STATUS.BORROWED.ToString())
            {
                return _apiResBuilder.Conflict("Book is already borrowed!");
            }

            // Add a new borrow record
            Tblborrows new_borrow = new Tblborrows
            {
                BorrowID = Guid.NewGuid(),
                BookID = borrow.BookId,
                MemberID = borrow.MemberId,
                BorrowStartDate = borrow.BorrowStartDate,
                BorrowEndDate = borrow.BorrowEndDate
            };
            await _crudBorrows.AddAsync(new_borrow, token);

            // Update the book status to "Borrowed"
            existing_book.Status = BOOK_STATUS.BORROWED.ToString();
            await _crudBooks.UpdateAsync(existing_book, token);
            await _commiter.SaveChangesAsync(token);

            return _apiResBuilder.Success();
        });

        public Task<APIResponse> ReturnBook(Guid bookId, CancellationToken token = default) => Execute(async () =>
        {
            Tblbooks? existing_book = await _crudBooks.GetByIdAsync(bookId, token);
            if (existing_book == null)
            {
                return _apiResBuilder.NotFound("Book not found!");
            }

            // Check if the book is already returned
            if (existing_book.Status == BOOK_STATUS.AVAILABLE.ToString())
            {
                return _apiResBuilder.Conflict("Book is already returned!");
            }

            // Update the book status to "Available"
            existing_book.Status = BOOK_STATUS.AVAILABLE.ToString();
            await _crudBooks.UpdateAsync(existing_book, token);

            // Update borrow record
            IReadOnlyList<Tblborrows>? borrowsList = await _crudBorrows.FindAsync(b => b.BookID == bookId && !b.IsReturned, token);
            if (borrowsList == null || borrowsList.Count == 0)
            {
                return _apiResBuilder.NotFound("Borrow record not found!");
            }
            Tblborrows existing_borrows = borrowsList.First();
            existing_borrows.IsReturned = true;

            await _crudBorrows.UpdateAsync(existing_borrows, token);
            await _commiter.SaveChangesAsync(token);

            return _apiResBuilder.Success();
        });
    }
}
