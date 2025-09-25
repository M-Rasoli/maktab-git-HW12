using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure
{
    public class ReviewRepository : IReviewRepository
    {
        
        public void AddNewReview(Review review)
        {
            using (var _context = new AppDbContext())
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
            }
        }

        public void DeleteReview(Review review)
        {
            using (var _context = new AppDbContext())
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }

        public List<Review> GetAllReviewsForBook(int bookId)
        {
            using (var _context = new AppDbContext())
            {
                var reviews = _context.Reviews
                    .Where(x => x.BookId == bookId)
                    .Include(u=>u.User)
                    .Include(b=>b.Book)
                    .ToList();
                return reviews;
            }
        }
        public List<Review> GetAllReviews()
        {
            using (var _context = new AppDbContext())
            {
                var reviews = _context.Reviews.Include(b=>b.Book).Include(u=>u.User).ToList();
                return reviews;
            }
        }
        public List<Review> GetAllReviewsByUserID(int userId)
        {
            using (var _context = new AppDbContext())
            {
                var reviews = _context.Reviews.Where(x=>x.UserId == userId)
                    .Include(b =>b.Book).ToList();
                return reviews;
            }
        }
        public Review GetReview(int ReviewId)
        {
            using (var _context = new AppDbContext())
            {
                var review = _context.Reviews.FirstOrDefault(x => x.Id == ReviewId);
                if (review == null)
                    review = null;
                return review;
            }
        }
        public void UpdateReview(Review review)
        {
            using (var _context = new AppDbContext())
            {
                var oldReview = _context.Reviews.FirstOrDefault(x => x.Id == review.Id);
                oldReview.Rating = review.Rating;
                oldReview.Comment = review.Comment;
                _context.SaveChanges();
            }
        }
        public void ConfirmReview(int reviewId)
        {
            using (var _context = new AppDbContext())
            {
                var oldReview = _context.Reviews.FirstOrDefault(x => x.Id == reviewId);
                oldReview.IsConfirmed = true;
                _context.SaveChanges();
            }
        }
    }
}
