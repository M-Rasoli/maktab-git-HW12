using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public  User User { get; set; }
        public int UserId { get; set; }
        public  Book Book { get; set; }
        public int BookId { get; set; }
        public  string BorrowingTime { get; set; } = DateTime.Now.ToString();
    }
}
