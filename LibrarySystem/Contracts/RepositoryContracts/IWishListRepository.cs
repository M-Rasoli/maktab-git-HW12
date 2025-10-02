using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.RepositoryContracts
{
    public interface IWishListRepository
    {
        void AddNewWishList(WishList wishList);
        void DeleteWishList(WishList wishList);
        void UpdateWishList(WishList wishList);
        WishList GetWishListWithId(int id);
        List<WishList> GetWishListWithUserID(int id);
        WishList GetWishListWithBookId(int Bookid);

    }
}
