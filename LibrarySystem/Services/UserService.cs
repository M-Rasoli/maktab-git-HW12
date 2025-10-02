using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Contracts.ServiceContracts;
using LibrarySystem.Entities;
using LibrarySystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _uRepo = new UserRepository();

        public int ChangeUserStatus(int id)
        { 
            if(!_uRepo.ChangeUserStatus(id))
            {
                throw new Exception("Wrong User ID");
            }
            return id;
        }
        public string ShowUserPenaltyAmount(int id)
        {
            var amount = _uRepo.GetUserPenaltyAmount(id);
            var user = _uRepo.GetUserById(id);

            var sb = new StringBuilder();


            sb.AppendLine("----------------------------------------------------------------");
            sb.AppendLine($"{"Username",-30}{"PenaltyAmount",-15}");
            sb.AppendLine("----------------------------------------------------------------");

                string username = user.Username.ToString();
                string penaltyAmountokId = amount.ToString();

                sb.AppendLine(
                    $"{username,-30}" +
                    $"{penaltyAmountokId,-15}"
                );

            sb.AppendLine("----------------------------------------------------------------");

            return sb.ToString();

        }
        public string ShowUsersBorrowedBooks(int userId)
        {
            // گرفتن لیست BorrowedBookهای کاربر
            var borrows = _uRepo.GetUsersBorrowedBooks(userId);

            if (borrows == null || !borrows.Any())
                return "No borrowed books.";

            var sb = new StringBuilder();

            // سرستون‌ها
            sb.AppendLine("-------------------------------------------------------------------------------------");
            sb.AppendLine($"{"BorrowId",-10}{"BookId",-8}{"Title",-15}{"Category",-15}{"Borrowed Time",-20}");
            sb.AppendLine("-------------------------------------------------------------------------------------");

            // ردیف‌ها
            foreach (var borrow in borrows)
            {
                string borrowId = borrow.Id.ToString();
                string bookId = borrow.BookId.ToString();
                string title = borrow.Book?.Title ?? "-";
                string category = borrow.Book?.Category?.BookCategory ?? "-";
                string borrowedTime = borrow.BorrowingTime;

                sb.AppendLine(
                    $"{borrowId,-10}" +
                    $"{bookId,-8}" +
                    $"{title,-15}" +
                    $"{category,-15}" +
                    $"{borrowedTime,-20}"
                );
            }

            sb.AppendLine("-------------------------------------------------------------------------------------");

            return sb.ToString();
        }
        public string ShowUsersList()
        {
            var users = _uRepo.GetUserList(); // فرض: متدی داری که لیست یوزرها رو برمی‌گردونه
            if (users == null || !users.Any())
                return "Empty";

            var sb = new StringBuilder();

            // سرستون‌ها
            sb.AppendLine("------------------------------------------------------------------------------------");
            sb.AppendLine(
                $"{"ID",-5}{"Username",-25}{"Role",-20}{"IsActive",-10}{"UserPenalty",15}"
            );
            sb.AppendLine("------------------------------------------------------------------------------------");

            // ردیف‌ها
            foreach (var user in users)
            {
                string role = user.Role.ToString();
                string active = user.IsActeive ? "Yes" : "No";

                sb.AppendLine(
                    $"{user.Id,-5}" +
                    $"{user.Username,-25}" +
                    $"{role,-20}" +
                    $"{active,-10}" +
                    $"{user.PenaltyAmount,15}"
                );
            }

            sb.AppendLine("------------------------------------------------------------------------------------");

            return sb.ToString();
        }
    }
}
