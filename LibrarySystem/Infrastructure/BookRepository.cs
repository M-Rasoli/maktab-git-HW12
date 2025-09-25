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
    public class BookRepository : IBookRepository
    {
        //private readonly AppDbContext _context;
        //public BookRepository()
        //{
        //    _context = new AppDbContext();
        //}
        public Book GetBookByName(string name)
        {
            using (var _context = new AppDbContext())
            {
                var book = _context.Books.FirstOrDefault(x=>x.Title.ToUpper() == name.ToUpper());
                if (book != null)
                    return book;
                return null;
            }
        }
        public void AddNewBook(Book book)
        {
            using (var _context = new AppDbContext())
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
        }

        public void DeleteBookById(int id)
        {
            using (var context = new AppDbContext()) // 👈 اینجا DbContext ساخته میشه
            {
                var book = GetBookById(id);
                context.Books.Remove(book);
                context.SaveChanges();
            }
        }

        public Book GetBookById(int id)
        {
            using (var context = new AppDbContext()) // 👈 اینجا DbContext ساخته میشه
            {
                var book = context.Books.Where(x => x.Id == id).FirstOrDefault();
                if (book != null)
                    return book;
                return null;
            }
        }

        public List<Book> GetBooksList()
        {
            using (var _context = new AppDbContext())
            {
                return _context.Books.ToList();
            }
        }
        public List<Book> GetBooksListWithCategories()
        {
            using (var _context = new AppDbContext())
            {
                var book = _context.Books.AsNoTracking()
                    .Include(x => x.Category)
                    .ToList();
                return book;
            }
        }
        public void UpdateBook(Book book)
        {
            using (var _context = new AppDbContext())
            {
                var upBook = _context.Books.FirstOrDefault(x => x.Id == book.Id);
                upBook.IsBorrowed = book.IsBorrowed;
                _context.SaveChanges();
            }
        }
    }
}
