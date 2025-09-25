using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.RepositoryContracts
{
    public interface IReviewRepository
    {
        void AddNewReview(Review review);
        List<Review> GetAllReviewsForBook(int bookId);
        List<Review> GetAllReviews();
        List<Review> GetAllReviewsByUserID(int userId);
        void DeleteReview(Review review);
        Review GetReview(int ReviewId);
        void UpdateReview(Review review);
        void ConfirmReview(int reviewId);

    }
}
