using LibrarySystem.Entities;
using LibrarySystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.RepositoryContracts
{
    public interface IBookRepository
    {
        void AddNewBook(Book book);
        Book GetBookById(int id);
        List<Book> GetBooksList();
        List<Book> GetBooksListWithCategories();
        void DeleteBookById(int id);
        void UpdateBook(Book book);
        Book GetBookByName(string name);

    }
}
