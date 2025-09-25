using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure
{
    public class AppDbContext : DbContext
    {
        #region Configuring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-I05OKD5\SQLEXPRESS;
                    Database=EfLibrarySystem;
                        Integrated Security=true;
                                TrustServerCertificate=true;");
        }

        #endregion

        #region ModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(x => x.Id);
            modelBuilder.Entity<User>()
                .Property(x => x.IsActeive)
                .HasDefaultValue(false);
            modelBuilder.Entity<User>()
                .HasMany(b => b.borrowedBooks)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(r => r.Reviews)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .HasMaxLength(150);

            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasKey(x => x.Id);
            modelBuilder.Entity<Category>()
                .HasMany(b => b.books)
                .WithOne(c => c.Category)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>()
                .Property(x => x.BookCategory)
                .HasMaxLength(150);

            modelBuilder.Entity<Book>()
                .ToTable("Books")
                .HasKey(x => x.Id);
            modelBuilder.Entity<Book>()
                .HasMany(x => x.Reviews)
                .WithOne(b => b.Book)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .HasMaxLength(150);

            modelBuilder.Entity<BorrowedBook>()
                .ToTable("BorrowedBooks")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Review>()
                .ToTable("Reviews")
                .HasKey(x => x.Id);
            modelBuilder.Entity<Review>(e =>
            {
                e.Property(x => x.UserId)
                .IsRequired();
                e.Property(x => x.BookId)
                .IsRequired();
            });
            modelBuilder.Entity<Review>()
                .Property(x => x.IsConfirmed)
                .HasDefaultValue(false);  

            modelBuilder.Entity<Category>().HasData(new List<Category>()
            {
                new Category() { Id = 1, BookCategory = "Historical" },
                new Category() { Id = 2, BookCategory = "Novel" },
                new Category() { Id = 3, BookCategory = "Criminal" }
            });

            modelBuilder.Entity<Book>().HasData(new List<Book>()
            {
                new Book(){Id = 1 , Title = "1776" ,IsBorrowed = false , CategoryId = 1},
                new Book(){Id = 2 , Title = "The Alchemist" ,IsBorrowed = false , CategoryId = 2},
                new Book(){Id = 3 , Title = "Broken" ,IsBorrowed = false, CategoryId = 3},
            });


            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Review> Reviews { get; set; }

        #endregion

    }
}
