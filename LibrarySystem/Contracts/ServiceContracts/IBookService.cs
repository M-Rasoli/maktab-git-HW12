using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.ServiceContracts
{
    public interface IBookService
    {
        List<Book> ShowBooks();
        string ShowBooksList();
        int AddNewBook(string title, int categoryId);
    }
}
