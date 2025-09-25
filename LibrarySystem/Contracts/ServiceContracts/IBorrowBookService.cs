using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.ServiceContracts
{
    public interface IBorrowBookService
    {
        int BorrowABook(int bookId,int usrId);
        void ReturnBook(int borrowId, int userId);
    }
}
