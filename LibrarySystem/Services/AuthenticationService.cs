using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Contracts.ServiceContracts;
using LibrarySystem.Entities;
using LibrarySystem.Enums;
using LibrarySystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DTOs;

namespace LibrarySystem.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        IUserRepository _userRepository = new UserRepository();
        public bool Login(string username, string password)
        {
            var user = _userRepository.GetUserByUserName(username);
            if (user != null)
            {
                if (user.Password == password)
                {
                    if (user.IsActeive == false)
                    {
                        throw new Exception("Activation is required.");
                    }
                    Session.LoggedInUser = user;
                    return true;
                }
                throw new Exception("Username Or Password is Worng");
            }
            throw new Exception("Username Or Password is Worng");
        }

        public int Register(AddUserDto user)
        {
            if(!CheckIfUserNameAlreadyExist(user.Username))
            {
                throw new Exception("Username Already Exist!");
            }

            return _userRepository.AddNewUser(user);
        }
        public bool CheckIfUserNameAlreadyExist(string username)
        {
            var user = _userRepository.GetUserByUserName(username);
            if (user != null)
                return false;
            return true;
    
        }

    }
}
