using LibrarySystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.ServiceContracts
{
    public interface IAuthenticationService
    {
        int Register(string username, string password, RoleEnum role);
        bool Login(string username, string password);
        bool CheckIfUserNameAlreadyExist(string username);
    }
}
