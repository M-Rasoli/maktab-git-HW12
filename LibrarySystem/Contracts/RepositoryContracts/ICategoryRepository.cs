using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.RepositoryContracts
{
    public interface ICategoryRepository
    {
        void AddNewCategory(Category category);
        List<Category> GetAllCategories();
        Category GetCategoryByName(string name);
        Category GetCategoryById(int Id);
    }
}
