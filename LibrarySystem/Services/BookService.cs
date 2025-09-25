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
    public class BookService : IBookService
    {
        IBookRepository _bookRepository = new BookRepository();
        ICategoryRepository _categoryRepository = new CategoryRepository();
        public List<Book> ShowBooks()
        {
            return _bookRepository.GetBooksListWithCategories();
        }
        public int AddNewBook(string title, int categoryId)
        {
            var checkBook = _bookRepository.GetBookByName(title);
            if(checkBook != null)
            {
                throw new Exception("Book Alredy Exist");
            }
            var checkCategory = _categoryRepository.GetCategoryById(categoryId);
            if (checkCategory == null)
            {
                throw new Exception("Invalid Category ID");
            }
            Book book = new Book();
            book.Title = title;
            book.CategoryId = categoryId;
            book.IsBorrowed = false;
            _bookRepository.AddNewBook(book);
            return book.Id;
        }
        public string ShowBooksList()
        {
            var books = ShowBooks();
            if (books == null || !books.Any())
                return "Empty";

            var sb = new StringBuilder();

            // سرستون‌ها
            sb.AppendLine("------------------------------------------------------------------------------------");
            sb.AppendLine(
                $"{"ID",-5}{"Title",-30}{"Category",-25}{"IsBorrowed",-12}"
            );
            sb.AppendLine("------------------------------------------------------------------------------------");

            // ردیف‌ها
            foreach (var item in books)
            {
                string category = item.Category?.BookCategory ?? "-";
                string borrowed = item.IsBorrowed ? "Yes" : "No";

                sb.AppendLine(
                    $"{item.Id,-5}" +
                    $"{item.Title,-30}" +
                    $"{category,-25}" +
                    $"{borrowed,-12}"
                );
            }

            sb.AppendLine("------------------------------------------------------------------------------------");

            return sb.ToString();
        }
    }
}
