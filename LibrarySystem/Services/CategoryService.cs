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
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository = new CategoryRepository();  
        public int AddNewCategory(string categoryName)
        {   
            var exisCategory = _categoryRepository.GetCategoryByName(categoryName);
            if(exisCategory != null)
            {
                throw new Exception("Category already exist!");
            }
            Category category = new Category();
            category.BookCategory = categoryName;
            _categoryRepository.AddNewCategory(category);
            return category.Id;
        }

        public string ShowCategoryList()
        {
            var categories = _categoryRepository.GetAllCategories();  
            if (categories == null || !categories.Any())
                return "Empty";

            var sb = new StringBuilder();

            // سرستون‌ها
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine(
                $"{"ID",-5}{"CategoryName",-30}"
            );
            sb.AppendLine("--------------------------------------------------");

            // ردیف‌ها
            foreach (var item in categories)
            {
                sb.AppendLine(
                    $"{item.Id,-5}" +
                    $"{item.BookCategory,-30}"
                );
            }

            sb.AppendLine("--------------------------------------------------");

            return sb.ToString();
        }
    }
}
