using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SchedulerDbContext _dbContext;
        public CategoryService(SchedulerDbContext dbContext){
            _dbContext = dbContext;
        }
        public async Task<object> CreateCategory(string name, User user)
        {
            var usersCategory = user.Categories.FirstOrDefault(category => category.Name == name);
            if (usersCategory != null){
                return "name of category must by unique";
            }

            var newCategory = new Category{
                Id = null,
                Name = name,
                Tasks = new List<Models.Task>(),
                User = user
            };

            await _dbContext.AddAsync(newCategory);
            await _dbContext.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category> DeleteCategory(int categoryId) // TODO callapse delete
        {
            var category = await GetCategorieById(categoryId);
            if (category == null)
                return null!;
            _dbContext.Categories.Remove((Category)category);
            await _dbContext.SaveChangesAsync();
            return (Category)category;
        }

        public async Task<Category?> GetCategorieById(int categoryId)
        {
            return await _dbContext.Categories.SingleOrDefaultAsync(category => category.Id.Equals(categoryId));
        }

        public async Task<IEnumerable<Category>> GetCategoriesByUserId(int userId)
        {
            return await _dbContext.Users.Where(user => user.Id == userId).SelectMany(user => user.Categories).ToListAsync();
        }

        public async Task<object> UpdateCategory(int categoryId, string name)
        {
            var category = await GetCategorieById(categoryId);
            if(category == null){
                return null!;
            }
            category.Name = name;
            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}