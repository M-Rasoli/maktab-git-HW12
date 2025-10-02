using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Enums;

namespace LibrarySystem.DTOs
{
    public class AddUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }

    }
}
