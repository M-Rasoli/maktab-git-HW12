using LibrarySystem.Entities;
using LibrarySystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.RepositoryContracts
{
    public interface IUserRepository
    {
        //CRUD
        void AddNewUser(User user);
        User GetUserById(int id);
        User GetUserByUserName(string userName);
        List<User> GetUserList();
        bool DeleteUserById(int id);
        bool ChangeUserStatus(int id);
        void UpdateUserbooks(User user);
        bool IsUserNameAlreadyExistUser(string userName);
        List<BorrowedBook> GetUsersBorrowedBooks(int userId);

    }
}
