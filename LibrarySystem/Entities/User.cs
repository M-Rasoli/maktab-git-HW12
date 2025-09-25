using LibrarySystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities
{
    public class User 
    {
        #region Ctor
        public User()
        {
            IsActeive = false;
        }

        #endregion

        #region Properties
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public bool IsActeive { get; set; }
        public List<BorrowedBook> borrowedBooks { get; set; } = new List<BorrowedBook>();
        public List<Review> Reviews { get; set; }
        #endregion


    }
}
