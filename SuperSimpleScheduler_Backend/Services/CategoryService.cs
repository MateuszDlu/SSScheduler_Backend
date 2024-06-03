using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<object> CreateCategory(string name, User user)
        {
            throw new NotImplementedException();
        }

        public Task<Category> DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategorieById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategoriesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateCategory(int categoryId, string name)
        {
            throw new NotImplementedException();
        }
    }
}