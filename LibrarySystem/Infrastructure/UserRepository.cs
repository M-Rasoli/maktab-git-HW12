using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Entities;
using LibrarySystem.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        //private readonly AppDbContext _context;
        //public UserRepository()
        //{
        //    _context = new AppDbContext();
        //}
        public User GetUserById(int id)
        {
            using (var _context = new AppDbContext())
            {
                var user = _context.Users.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
                if (user != null)
                    return user;
                return null;
            }
        }
        public bool ChangeUserStatus(int id)
        {
            using (var _context = new AppDbContext())
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    user.IsActeive = !user.IsActeive;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void AddNewUser(User user)
        {
            using (var _context = new AppDbContext())
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }

        public bool DeleteUserById(int id)
        {
            using (var _context = new AppDbContext())
            {
                var user = GetUserById(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public User GetUserByUserName(string userName)
        {
            using (var _context = new AppDbContext())
            {
                var user = _context.Users.AsNoTracking().Where(x => x.Username.ToUpper() == userName.ToUpper()).FirstOrDefault();
                if (user != null)
                    return user;
                return null;
            }
            
        }

        public List<User> GetUserList()
        {
            using (var _context = new AppDbContext())
            {
                return _context.Users.AsNoTracking().ToList();
            }
        }
        public bool IsUserNameAlreadyExistUser(string userName)
        {
            using (var _context = new AppDbContext())
            {
                var user = _context.Users.AsNoTracking().Where(x => x.Username.ToUpper() == userName.ToUpper()).FirstOrDefault();
                if (user == null)
                {
                    return true;
                }
                return false;
            }
        }

        public void UpdateUserbooks(User user)
        {
            using (var _context = new AppDbContext())
            {
                var u = GetUserById(user.Id);
                if (u != null)
                {
                    u.borrowedBooks = user.borrowedBooks;
                    _context.SaveChanges();
                }
            }
        }
        public List<BorrowedBook> GetUsersBorrowedBooks(int userId)
        {
            using (var _context = new AppDbContext())
            {
                var borrowedBooks = _context.BorrowedBooks
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Include(x => x.Book)
                .ThenInclude(b => b.Category)
                .ToList();
                return borrowedBooks;
            }
        }

    }
}
