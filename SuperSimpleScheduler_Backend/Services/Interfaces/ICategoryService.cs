using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId);
        public Task<Category> GetCategorieByIdAsync(int categoryId);
        public Task<Category> DeleteCategoryAsync(int categoryId);
        public Task<Object> UpdateCategoryAsync(int categoryId, string name);
        public Task<Object> CreateCategoryAsync(string name, User user);
    }
}