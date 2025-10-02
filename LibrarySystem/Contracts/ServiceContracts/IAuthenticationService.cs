using LibrarySystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DTOs;

namespace LibrarySystem.Contracts.ServiceContracts
{
    public interface IAuthenticationService
    {
        int Register(AddUserDto user);
        bool Login(string username, string password);
        bool CheckIfUserNameAlreadyExist(string username);
    }
}
