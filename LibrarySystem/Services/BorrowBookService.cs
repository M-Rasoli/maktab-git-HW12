using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Contracts.ServiceContracts;
using LibrarySystem.Entities;
using LibrarySystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class BorrowBookService : IBorrowBookService
    {
        private readonly IBorrowBookRepository _borrowRepo;
        private readonly IBookRepository _bookRepo;
        public BorrowBookService()
        {
            _bookRepo = new BookRepository();
            _borrowRepo = new BorrowBookRepository();
        }
        public int BorrowABook(int bookId, int usrId)
        {
            var book = _bookRepo.GetBookById(bookId);
            if (book == null)
                throw new Exception("Invalid ID");
            if(book.IsBorrowed == true)
            {
                throw new Exception("Book IS Already Borrowed!");
            }
            BorrowedBook borrowedBook = new BorrowedBook();
            borrowedBook.BookId = bookId;
            borrowedBook.UserId = usrId;
            _borrowRepo.AddBorrowedBook(borrowedBook);
            return borrowedBook.Id;

        }
        public void ReturnBook(int borrowId , int userId)
        {
            var borrow = _borrowRepo.GetBorrowedBookById(borrowId);
            if(borrow == null|| borrow.UserId != userId)
            {
                throw new Exception("Invalid BorrowBook ID");
            }
            _borrowRepo.DeleteBorrowedBook(borrow);

        }
    }
}
