using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure
{
    internal class WishListRepository : IWishListRepository
    {
        public void AddNewWishList(WishList wishList)
        {
            using(var _context = new AppDbContext())
            {
                _context.WishLists.Add(wishList);
                _context.SaveChanges();
            }
        }

        public void DeleteWishList(WishList wishList)
        {
            using (var _context = new AppDbContext())
            {
                _context.WishLists.Remove(wishList);
                _context.SaveChanges();
            }
        }

        public WishList GetWishListWithBookId(int Bookid)
        {
            using (var _context = new AppDbContext())
            {
                var wishList = _context.WishLists.FirstOrDefault(w => w.BookId == Bookid);
                if (wishList == null)
                    return null;
                return wishList;
            }
        }

        public WishList GetWishListWithId(int id)
        {
            using( var _context = new AppDbContext())
            {
                var wishList = _context.WishLists.FirstOrDefault(w => w.Id == id);
                if (wishList == null)
                    return null;
                return wishList;
            }
        }

        public List<WishList> GetWishListWithUserID(int id)
        {
            using(var _context = new AppDbContext())
            {
                return _context.WishLists
                    .Where(x => x.UserId == id)
                    .Include(b => b.Book)
                    .Include(u => u.User)
                    .ToList();
            }
        }

        public void UpdateWishList(WishList wishList)
        {
            throw new NotImplementedException();
        }
    }
}
