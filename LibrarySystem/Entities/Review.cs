using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
        public bool IsConfirmed { get; set; }
    }
}
