using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.RepositoryContracts
{
    public interface IBorrowBookRepository
    {
        void AddBorrowedBook(BorrowedBook borrowed);
        BorrowedBook GetBorrowedBookById(int id);
        List<BorrowedBook> GetBorrowedBooks();
        void DeleteBorrowedBook(BorrowedBook borrowed);

    }
}
