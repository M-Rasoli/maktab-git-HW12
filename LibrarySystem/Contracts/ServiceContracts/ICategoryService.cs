using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Contracts.ServiceContracts
{
    public interface ICategoryService
    {
        int AddNewCategory(string categoryName);
        string ShowCategoryList();
    }
}
