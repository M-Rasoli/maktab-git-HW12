using LibrarySystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string BookCategory { get; set; }
        public List<Book> books { get; set; } = new List<Book>();
    }
}
