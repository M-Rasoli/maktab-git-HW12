using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.ServiceContracts
{
    public interface IUserService
    {
        string ShowUsersBorrowedBooks(int userId);
        string ShowUsersList();
        int ChangeUserStatus(int id);
        string ShowUserPenaltyAmount(int id);
    }
}
