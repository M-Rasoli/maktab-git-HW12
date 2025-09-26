using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Contracts.ServiceContracts;
using LibrarySystem.Entities;
using LibrarySystem.Infrastructure;
using LibrarySystem.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class ReviewService : IReviewService
    {
        IBookRepository _bookRepository = new BookRepository();
        IUserRepository _userRepository = new UserRepository();
        IReviewRepository _reviewRepository = new ReviewRepository();
        public int AddNewReview(int bookID, int userID, string comment,int rate)
        {
            var user = _userRepository.GetUserById(userID);
            if (user == null)
            {
                throw new Exception("Wrong User ID");
            }
            var book = _bookRepository.GetBookById(bookID);
            if (book == null)
            {
                throw new Exception("Wrong Book ID");
            }
            Review review = new Review();
            review.BookId = bookID;
            review.UserId = userID;
            review.Rating = rate;
            review.Comment = comment ?? "-";
            _reviewRepository.AddNewReview(review);
            return review.Id;

        }
        public int UpdateReview(int reviewID, int userId, int newRating, string newComment)
        {
            var review = _reviewRepository.GetReview(reviewID);
            if (review == null)
            {
                throw new Exception("Wrong Review ID");
            }
            if (review.UserId != userId)
            {
                throw new Exception("This Review IS NOT for You .");
            }
            review.Rating = newRating;
            review.Comment = newComment;
            _reviewRepository.UpdateReview(review);
            return review.Id;

        }
        public int ConfirmReview(int reviewID)
        {
            var review = _reviewRepository.GetReview(reviewID);
            if (review == null)
            {
                throw new Exception("Invalid Review ID");
            }
            if (review.IsConfirmed == true)
            {
                throw new Exception("Review IS Already Confirmed");
            }
            _reviewRepository.ConfirmReview(reviewID);
            return reviewID;
        }

        public void DeleteReview(int reviewId, int userId)
        {
            var review = _reviewRepository.GetReview(reviewId);
            if (review == null)
            {
                throw new Exception("Wrong Review ID");
            }
            if (review.UserId != userId)
            {
                throw new Exception("This Review IS NOT for You .");
            }
            _reviewRepository.DeleteReview(review);
        }
        public string GetAllReviewsByUserID(int userId)
        {
            var usersReview = _reviewRepository.GetAllReviewsByUserID(userId);

            if (usersReview == null || !usersReview.Any())
                return "Empty";

            var sb = new StringBuilder();

            // سرستون‌ها (اضافه کردن Confirmed)
            sb.AppendLine("------------------------------------------------------------------------------------------------------");
            sb.AppendLine(
                $"{"ID",-5}{"Book",-15}{"Rating",-8}{"CreatedAt",-20}{"Comment",-30}{"Confirmed",-10}"
            );
            sb.AppendLine("------------------------------------------------------------------------------------------------------");

            // ردیف‌ها
            foreach (var review in usersReview)
            {
                string bookTitle = review.Book?.Title ?? "-";
                string comment = string.IsNullOrWhiteSpace(review.Comment) ? "-" : review.Comment;
                string confirmed = review.IsConfirmed ? "Yes" : "No";  // وضعیت IsConfirmed

                sb.AppendLine(
                    $"{review.Id,-5}" +
                    $"{bookTitle,-15}" +
                    $"{review.Rating,-8}" +
                    $"{review.CreatedAt,-20}" +
                    $"{comment,-30}" +
                    $"{confirmed,-10}"
                );
            }

            sb.AppendLine("------------------------------------------------------------------------------------------------------");

            return sb.ToString();
        }
        public string ShowReviewsListAdmin()
        {

            var reviews = _reviewRepository.GetAllReviews().Where(x=>x.IsConfirmed == false);

            if (reviews == null || !reviews.Any())
                return "Empty";

            var sb = new StringBuilder();

            // سرستون‌ها
            sb.AppendLine("--------------------------------------------------------------------------------------------------");
            sb.AppendLine(
                $"{"ID",-5}{"User",-20}{"Book",-15}{"Rating",-8}{"CreatedAt",-25}{"Comment",-30}"
            );
            sb.AppendLine("--------------------------------------------------------------------------------------------------");

            // ردیف‌ها
            foreach (var review in reviews)
            {
                string userName = review.User.Username;      // اگر کاربر null بود "-"
                string bookTitle = review.Book.Title;       // اگر کتاب null بود "-"
                string comment = string.IsNullOrWhiteSpace(review.Comment) ? "-" : review.Comment;

                sb.AppendLine(
                    $"{review.Id,-5}" +
                    $"{userName,-20}" +
                    $"{bookTitle,-15}" +
                    $"{review.Rating,-8}" +
                    $"{review.CreatedAt,-25}" +
                    $"{comment,-30}"
                );
            }

            sb.AppendLine("--------------------------------------------------------------------------------------------------");

            return sb.ToString();
        }
        public string ShowConfirmedReviewsList(int bookId)
        {
            var reviews = _reviewRepository.GetAllReviewsForBook(bookId)
                                  .Where(r => r.IsConfirmed)
                                  .ToList();

            if (reviews == null || !reviews.Any())
                return "Empty";

            var sb = new StringBuilder();

            // سرستون‌ها
            sb.AppendLine("--------------------------------------------------------------------------------------------------");
            sb.AppendLine(
                $"{"ID",-5}{"User",-20}{"Book",-15}{"Rating",-8}{"CreatedAt",-25}{"Comment",-30}"
            );
            sb.AppendLine("--------------------------------------------------------------------------------------------------");

            // ردیف‌ها
            foreach (var review in reviews)
            {
                string userName = review.User.Username;   
                string bookTitle = review.Book.Title;     
                string comment = string.IsNullOrWhiteSpace(review.Comment) ? "-" : review.Comment;

                sb.AppendLine(
                    $"{review.Id,-5}" +
                    $"{userName,-20}" +
                    $"{bookTitle,-15}" +
                    $"{review.Rating,-8}" +
                    $"{review.CreatedAt,-25}" +
                    $"{comment,-30}"
                );
            }

            sb.AppendLine("--------------------------------------------------------------------------------------------------");

            
            double avgRating = reviews.Average(r => (double)r.Rating);
            sb.AppendLine($"📊 Average Rates : {avgRating:F2}");

            return sb.ToString();
        }

    }
}
