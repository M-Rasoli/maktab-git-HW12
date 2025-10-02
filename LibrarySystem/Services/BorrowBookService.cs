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
    public class BorrowBookService() : IBorrowBookService
    {
        private readonly IBorrowBookRepository _borrowRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;
        public BorrowBookService(UnitOfWork _unit) : this()
        {
            _bookRepo = new BookRepository();
            _borrowRepo = new BorrowBookRepository(_unit);
            _userRepo = new UserRepository();
        }
        public int BorrowABook(int bookId, int usrId)
        {
            var book = _bookRepo.GetBookById(bookId);
            if (book == null)
                throw new Exception("Invalid ID");
            if (book.IsBorrowed == true)
            {
                throw new Exception("Book IS Already Borrowed!");
            }
            BorrowedBook borrowedBook = new BorrowedBook();
            borrowedBook.BookId = bookId;
            borrowedBook.UserId = usrId;
            _borrowRepo.AddBorrowedBook(borrowedBook);
            return borrowedBook.Id;

        }
        public void ReturnBook(int borrowId, int userId)
        {
            var borrow = _borrowRepo.GetBorrowedBookById(borrowId);
            if (borrow == null || borrow.UserId != userId)
            {
                throw new Exception("Invalid BorrowBook ID");
            }
            var penalty = CheckPenalty(borrowId);
            var Id = borrow.UserId;
            //null refrense
            if (penalty > 0.0)
            {
                _userRepo.ChangeUserPenaltyAmount(penalty, Id);
            }
            _borrowRepo.DeleteBorrowedBook(borrow);

        }
        public float CheckPenalty(int borrowId)
        {
            var borrow = _borrowRepo.GetBorrowedBookById(borrowId);
            var borrowTime = DateTime.Parse(borrow.BorrowingTime);
            var calculatePassedDay = DateTime.Now.Day - borrowTime.Day;
            var calculate = Math.Abs(calculatePassedDay);
            var penalty = 0.0;
            if (calculate > 7)
            {
                calculate = 7 - calculate;
                penalty = borrow.Penalty * calculate;
                
            }
            return (float)Math.Abs(penalty);
        }
    }
}
