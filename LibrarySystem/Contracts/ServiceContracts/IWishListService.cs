using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.ServiceContracts
{
    public interface IWishListService
    {
        int AddNewWishList(int bookId, int userId);
        int DeleteWishList(int wishListId, int userId);
        string ShowUserWishlist(int userId);
        bool CheckIfUserAlredyHasWishList(int BookId, int userId);
    }
}
