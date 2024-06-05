using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategoriesByUserId(int userId);
        public Task<Category> GetCategorieById(int categoryId);
        public Task<Category> DeleteCategory(int categoryId);
        public Task<Object> UpdateCategory(int categoryId, string name);
        public Task<Object> CreateCategory(string name, User user);
    }
}