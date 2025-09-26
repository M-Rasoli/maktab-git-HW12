// See https://aka.ms/new-console-template for more information
using Azure.Identity;
using LibrarySystem;
using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Contracts.ServiceContracts;
using LibrarySystem.Entities;
using LibrarySystem.Enums;
using LibrarySystem.Infrastructure;
using LibrarySystem.Services;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Identity.Client;
using Spectre.Console;
using System.Transactions;

Console.WriteLine("Hello, World!");

IAuthenticationService authenticationservice = new AuthenticationService();
IBookService bookService = new BookService();
IBorrowBookService borrowBookService = new BorrowBookService();
IUserService userService = new UserService();
ICategoryService categoryService = new CategoryService();
IReviewService reviewService = new ReviewService();
IWishListService wishListService = new WishListService();
//var date = DateTime.Now.ToString();
//var sDate = DateTime.Parse(date);
//Console.WriteLine(sDate);
//var s = sDate.Day;
//var sDateN = sDate.AddDays(7);
//Console.WriteLine(s);
//Console.WriteLine(sDateN);
//Console.ReadKey();

while (true)
{
    Console.Clear();

    var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[mediumspringgreen]🔰 Select an action:[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more actions)[/]")
            .AddChoices(new[]
            {
                        "Login",
                        "Register",
                        "Exit"
            }));

    switch (choice)
    {
        case "Register":
            HandleRegister();
            break;

        case "Login":
            HandleLogin();
            break;

        case "Exit":
            AnsiConsole.MarkupLine("[red]Exiting... Goodbye![/]");
            return;
    }
}

void HandleRegister()
{
    Console.Clear();
    Console.WriteLine("What role would you like to register as?");
    Console.WriteLine("|-|-|-|-(1 - User) || (2 - Admin)-|-|-|-|");
    string roleInput = Console.ReadLine()!;
    bool checkRole = int.TryParse(roleInput, out int role);
    var username = AnsiConsole.Ask<string>("Enter your [green]USERNAME[/]:");
    var password = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter your [green]PASSWORD[/]:")
            .PromptStyle("red")
            .Secret());
    try
    {
        var id = authenticationservice.Register(username, password, (RoleEnum)role);
        AnsiConsole.MarkupLine($"[yellow]User {username} with ID {id} registered successfully![/]");
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
    }
}

void HandleLogin()
{
    Console.Clear();
    var username = AnsiConsole.Ask<string>("Enter your [green]USERNAME[/]:");
    var password = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter your [green]PASSWORD[/]:")
            .PromptStyle("red")
            .Secret());

    try
    {
        authenticationservice.Login(username, password);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        return;
    }
    switch (Session.LoggedInUser.Role)
    {
        case RoleEnum.Admin:
            AdminMenu();
            break;

        case RoleEnum.User:
            UserMenu();
            break;
    }
}

void UserMenu()
{
    do
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[yellow]Welcome back, {Session.LoggedInUser.Username}![/]");
        var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[mediumspringgreen]🔰 Select an action:[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more actions)[/]")
            .AddChoices(new[]
            {
                        "View list of books",
                        "Borrowing a book",
                        "View the list of borrowed books",
                        "Add a Review For Book",
                        "Change Or Delete Review For Book",
                        "Penalty Amount Status",
                        "Wish List",
                        "Log Out"
            }));
        switch (choice)
        {
            case "View list of books":
                var books = bookService.ShowBooksList();
                Console.WriteLine(books);
                Console.WriteLine("Choose a Book Id To Display Reviews (00 - For Back)");
                var inDisRev = Console.ReadLine()!;
                if (inDisRev == "00")
                {
                    break;
                }
                bool checkDisRev = int.TryParse(inDisRev, out var numDisRev);
                try
                {
                    var reviewsForBook = reviewService.ShowConfirmedReviewsList(numDisRev);
                    Console.WriteLine(reviewsForBook);
                    Console.WriteLine("Want to Add This Book To Your Wish List? If yes Enter Book ID (00-for Back) ");
                    var inWish = Console.ReadLine()!;
                    if (inWish == "00")
                    {
                        break;
                    }
                    bool checkInWish = int.TryParse(inWish, out int inWishNum);
                    try
                    {
                        var wishID = wishListService.AddNewWishList(inWishNum, Session.LoggedInUser.Id);
                        AnsiConsole.MarkupLine($"[green]Book with ID {inWishNum} Added To Your Wish List[/]");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }

                break;

            case "Borrowing a book":
                Console.Clear();
                books = bookService.ShowBooksList();
                Console.WriteLine(books);
                Console.WriteLine("Select an ID to borrow. (00 - For Back)");
                string inBook = Console.ReadLine()!;
                if (inBook == "00")
                {
                    break;
                }
                bool chbook = int.TryParse(inBook, out var bookNumber);
                try
                {
                    var borrowid = borrowBookService.BorrowABook(bookNumber, Session.LoggedInUser.Id);
                    AnsiConsole.MarkupLine($"[yellow]You Borrowed Book with ID {bookNumber}[/]");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                break;

            case "View the list of borrowed books":
                var borrowedBooks = userService.ShowUsersBorrowedBooks(Session.LoggedInUser.Id);
                Console.WriteLine(borrowedBooks);
                Console.WriteLine("Enter Borrow ID to Return A book (00 - for back)");
                string inBorrow = Console.ReadLine()!;
                if (inBorrow == "00")
                {
                    break;
                }
                bool chborrow = int.TryParse(inBorrow, out var borrowNumber);
                try
                {
                    borrowBookService.ReturnBook(borrowNumber, Session.LoggedInUser.Id);
                    Console.WriteLine("Done!");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                Console.ReadKey();
                break;

            case "Add a Review For Book":
                var bookList = bookService.ShowBooksList();
                Console.WriteLine(bookList);
                Console.WriteLine("Enter a Book ID to Add a Review : (00 - for back)");
                var inBookIdRev = Console.ReadLine()!;
                if (inBookIdRev == "00")
                {
                    break;
                }
                bool checkInBookRev = int.TryParse(inBookIdRev, out var bookNumberRev);
                Console.WriteLine("Enter Your Rate For Book (1 - 5) : (00 - for back)");
                var inVote = Console.ReadLine()!;
                if (inVote == "00")
                {
                    break;
                }
                bool checkVote = int.TryParse(inVote, out int numVote);
                if (numVote < 1 || numVote > 5)
                {
                    Console.WriteLine("You Can Rate Between ( 1 to 5 )");
                    Console.ReadKey();
                    break;
                }
                Console.WriteLine("(Unnecessary) : Enter Comment For This Book");
                var inComment = Console.ReadLine()!;
                try
                {
                    var numReview = reviewService.AddNewReview(bookNumberRev, Session.LoggedInUser.Id, inComment, numVote);
                    AnsiConsole.MarkupLine($"[yellow]Your Review Will Be Submitted After confirmation with ID {numReview}[/]");
                    Console.ReadKey();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                break;

            case "Change Or Delete Review For Book":
                var revList = reviewService.GetAllReviewsByUserID(Session.LoggedInUser.Id);
                Console.WriteLine(revList);
                Console.ReadKey();
                if (revList.ToLower() == "Empty".ToLower())
                {
                    break;
                }
                Console.WriteLine("Choose A Review ID For Change Or Delete : (00 - for back)");
                var inChangerev = Console.ReadLine()!;
                if (inChangerev == "00")
                {
                    break;
                }
                bool checkRev = int.TryParse(inChangerev, out int numRev);
                Console.WriteLine("You Want To Change or Delete This Review? (1 For Change - 2 For Delete ) (00 - for back)");
                var inChDel = Console.ReadLine()!;
                if (inChDel == "00")
                {
                    break;
                }
                bool checkChDel = int.TryParse(inChDel, out int numChDel);
                if (numChDel == 1)
                {
                    Console.WriteLine("Enter New Rate");
                    var inNewRate = Console.ReadLine()!;
                    bool checkNewRate = int.TryParse(inNewRate, out int newRateNum);
                    Console.WriteLine("Enter New Comment");
                    var inNewComment = Console.ReadLine()!;
                    try
                    {
                        var newRate = reviewService.UpdateReview(numRev, Session.LoggedInUser.Id, newRateNum, inNewComment);
                        AnsiConsole.MarkupLine($"[yellow]Your Review Changed Successfully with ID {newRate}[/]");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
                if (numChDel == 2)
                {
                    try
                    {
                        reviewService.DeleteReview(numRev, Session.LoggedInUser.Id);
                        Console.WriteLine("Done!");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
                break;

            case "Penalty Amount Status":
                var pAmount = userService.ShowUserPenaltyAmount(Session.LoggedInUser.Id);
                Console.WriteLine(pAmount);
                Console.WriteLine("Press Any Key To Back");
                Console.ReadKey();
                break;
            case "Wish List":
                var userWishList = wishListService.ShowUserWishlist(Session.LoggedInUser.Id);
                Console.WriteLine(userWishList);
                Console.WriteLine("If You Want To Delete a Book From Wish list Enter Wish List Id : (00 - for Back)");
                var inDelWish = Console.ReadLine()!;
                if (inDelWish == "00")
                {
                    break;
                }
                bool checkDelWish = int.TryParse(inDelWish, out int numDelWish);
                try
                {
                    var idWishDel = wishListService.DeleteWishList(numDelWish,Session.LoggedInUser.Id);
                    Console.WriteLine("Done !");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                Console.ReadKey();
                break;

            case "Log Out":
                return;
        }
    } while (true);

}
void AdminMenu()
{
    do
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[yellow]Welcome back, {Session.LoggedInUser.Username}![/]");
        var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[mediumspringgreen]🔰 Select an action:[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more actions)[/]")
            .AddChoices(new[]
            {
                        "Add new Category",
                        "Add new Book",
                        "View List Of Books",
                        "View List Of Categories",
                        "Active user",
                        "Confirm a Review",
                        "Log Out"
            }));
        switch (choice)
        {
            case "View List Of Books":
                var books = bookService.ShowBooksList();
                Console.WriteLine(books);
                Console.WriteLine("Press Any Key For Back");
                Console.ReadKey();
                break;

            case "Add new Category":
                Console.Clear();
                var cate = categoryService.ShowCategoryList();
                Console.WriteLine(cate);
                Console.Write("Enter New Category name : (00 - for back)");
                string categoryName = Console.ReadLine()!;
                if (categoryName == "00")
                {
                    break;
                }
                if (categoryName == null || categoryName == " " || categoryName == "")
                {
                    AnsiConsole.MarkupLine($"[red]Category Name Cant Be null [/]");
                    Console.ReadLine();
                    break;
                }
                try
                {
                    var categoryId = categoryService.AddNewCategory(categoryName);
                    AnsiConsole.MarkupLine($"[green]Category with ID {categoryId} Created [/]");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                    Console.ReadKey();
                }
                break;

            case "Add new Book":
                Console.Clear();
                var bookList = bookService.ShowBooksList();
                Console.WriteLine(bookList);
                Console.WriteLine("Enter New BookName to Add : (00 for back)");
                var inbooktitle = Console.ReadLine()!;
                if (inbooktitle == "00")
                {
                    break;
                }
                if (inbooktitle == null || inbooktitle == " " || inbooktitle == "")
                {
                    AnsiConsole.MarkupLine($"[red]Book Name Cant Be null [/]");
                    Console.ReadLine();
                    break;
                }
                var categoryList = categoryService.ShowCategoryList();
                Console.WriteLine(categoryList);
                Console.WriteLine("Enter Category ID For Book : (00 for back)");
                var incate = Console.ReadLine()!;
                if (incate == "00")
                {
                    break;
                }
                bool checkincate = int.TryParse(incate, out int categorynum);
                try
                {
                    var newBookId = bookService.AddNewBook(inbooktitle, categorynum);
                    AnsiConsole.MarkupLine($"[green]Book with ID {newBookId} Added [/]");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                break;



            case "View List Of Categories":
                var categories = categoryService.ShowCategoryList();
                Console.WriteLine(categories);
                Console.WriteLine("Press Any Key For Back");
                Console.ReadKey();
                break;

            case "Active user":
                var users = userService.ShowUsersList();
                Console.WriteLine(users);
                Console.WriteLine("Enter User ID to Change Status (00 - for back)");
                string inUser = Console.ReadLine()!;
                if (inUser == "00")
                {
                    break;
                }
                bool chUser = int.TryParse(inUser, out var userNumber);
                try
                {
                    userService.ChangeUserStatus(userNumber);
                    Console.WriteLine("Done!");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                break;

            case "Confirm a Review":
                var reviews = reviewService.ShowReviewsListAdmin();
                Console.WriteLine(reviews);
                Console.WriteLine("Enter Review ID To Confirm : (00 - for back)");
                var inrev = Console.ReadLine()!;
                if (inrev == "00")
                {
                    break;
                }
                bool chInRev = int.TryParse(inrev, out int revId);
                try
                {
                    var revnum = reviewService.ConfirmReview(revId);
                    AnsiConsole.MarkupLine($"[green]Review with ID {revnum} Confirmed [/]");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                break;


            case "Log Out":

                return;
        }
    } while (true);

}


