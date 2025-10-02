using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure
{
    public class BorrowBookRepository() : IBorrowBookRepository
    {

        public void AddBorrowedBook(BorrowedBook borrowed)
        {
            using (var _context = new AppDbContext()) // 👈 اینجا DbContext ساخته میشه
            {
                _context.BorrowedBooks.Add(borrowed);
                var book = _context.Books.FirstOrDefault(x => x.Id == borrowed.BookId);
                book.IsBorrowed = true;
                //var user = _context.Users.FirstOrDefault(x => x.Id == borrowed.UserId);
                //user.borrowedBooks.Add(borrowed);
                _context.SaveChanges();
            }
        }

        public void DeleteBorrowedBook(BorrowedBook borrowed)
        {
            using (var _context = new AppDbContext())
            {
                _context.BorrowedBooks.Remove(borrowed);
                var book = _context.Books.FirstOrDefault(x => x.Id == borrowed.BookId);
                book.IsBorrowed = false;
                //var user = _context.Users.FirstOrDefault(x => x.Id == borrowed.UserId);
                //user.borrowedBooks.Remove(borrowed);
                _context.SaveChanges();
            }
        }
        //public List<BorrowedBook> GetUsersBorrowedBooks(int userId)
        //{
        //    var books = _context.BorrowedBooks.FirstOrDefault(x => x.UserId == userId);
        //    return books.ToString(); 
        //}

        public BorrowedBook GetBorrowedBookById(int id)
        {
            using (var _context = new AppDbContext())
            {
                var book = _context.BorrowedBooks.FirstOrDefault(x => x.Id == id);
                if (book != null)
                    return book;
                return null;
            }
        }

        public List<BorrowedBook> GetBorrowedBooks()
        {
            using (var _context = new AppDbContext())
            {
                return _context.BorrowedBooks.ToList();
            }
        }
    }
}
