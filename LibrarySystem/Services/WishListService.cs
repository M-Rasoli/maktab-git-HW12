using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Contracts.ServiceContracts;
using LibrarySystem.Entities;
using LibrarySystem.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _wishRepo;
        public WishListService()
        {
            _wishRepo = new WishListRepository();
        }
        public int AddNewWishList(int bookId, int userId)
        {
            WishList wishList = new WishList();
            wishList.BookId = bookId;
            wishList.UserId = userId;
            if(CheckIfUserAlredyHasWishList(bookId, userId))
                _wishRepo.AddNewWishList(wishList);
            return wishList.Id;
        }
        public bool CheckIfUserAlredyHasWishList(int BookId ,int userId)
        {
            var wish = _wishRepo.GetWishListWithBookId(BookId);
            if (wish == null) return true;
            if(wish.UserId == userId)
            {
                throw new Exception("This Book Is Already In Ur Wish List");
            }
            return true;

        }

        public int DeleteWishList(int wishListId, int userId)
        {
            var wishList = _wishRepo.GetWishListWithId(wishListId);
            if (wishList == null)
            {
                throw new Exception("Wrong WishList ID");
            }
            if (wishList.UserId != userId)
            {
                throw new Exception("This Wish IS NOT for You .");
            }
            _wishRepo.DeleteWishList(wishList);
            return wishList.Id;
        }


        public string ShowUserWishlist(int userId)
        {
            // گرفتن لیست وی‌ش‌لیست‌های کاربر
            var wishlists = _wishRepo.GetWishListWithUserID(userId);

            if (wishlists == null || !wishlists.Any())
                return "No wishlist items.";

            var sb = new StringBuilder();

            // سرستون‌ها
            sb.AppendLine("-------------------------------------------------------------------------------------");
            sb.AppendLine($"{"WishId",-8}{"UserName",-15}{"BookTitle",-20}{"CreatedAt",-20}");
            sb.AppendLine("-------------------------------------------------------------------------------------");

            // ردیف‌ها
            foreach (var item in wishlists)
            {
                string wishId = item.Id.ToString();
                string userName = item.User.Username;
                string bookTitle = item.Book.Title;
                string createdAt = item.CreatedAt;

                sb.AppendLine(
                    $"{wishId,-8}" +
                    $"{userName,-15}" +
                    $"{bookTitle,-20}" +
                    $"{createdAt,-20}"
                );
            }

            sb.AppendLine("-------------------------------------------------------------------------------------");

            return sb.ToString();
        }

    }
}
