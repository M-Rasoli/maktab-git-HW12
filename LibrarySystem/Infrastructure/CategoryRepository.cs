using LibrarySystem.Contracts.RepositoryContracts;
using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        //private readonly AppDbContext _context;
        //public CategoryRepository()
        //{
        //    _context = new AppDbContext();
        //}
        public void AddNewCategory(Category category)
        {
            using (var _context = new AppDbContext())
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
        }
        public Category GetCategoryById(int Id)
        {
            using (var _context = new AppDbContext())
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == Id);
                if (category == null)
                    return null;
                return category;
            }
        }
        public List<Category> GetAllCategories()
        {
            using (var _context = new AppDbContext())
            {
                return _context.Categories.ToList();
            }
        }
        public Category GetCategoryByName(string name)
        {
            using (var _context = new AppDbContext())
            {
                var category = _context.Categories.FirstOrDefault(x => x.BookCategory.ToUpper() == name.ToUpper());
                if (category == null)
                    return null;
                return category;
            }

        }

    }
}
