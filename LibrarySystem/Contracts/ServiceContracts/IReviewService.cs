using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.ServiceContracts
{
    public interface IReviewService
    {
        int AddNewReview(int bookID, int userID , string comment, int rate);
        void DeleteReview(int reviewId, int userId);
        string ShowConfirmedReviewsList(int bookId);
        int UpdateReview(int reviewID,int userId,int newRating , string newComment);
        int ConfirmReview(int reviewID);
        string ShowReviewsListAdmin();
        string GetAllReviewsByUserID(int userId);
    }
}
