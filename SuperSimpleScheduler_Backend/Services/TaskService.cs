using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public class TaskService : ITaskService
    {
        private readonly SchedulerDbContext _dbContext;
        private readonly ICategoryService _categoryService;
        public TaskService(SchedulerDbContext dbContext, ICategoryService categoryService){
            _dbContext = dbContext;
            _categoryService = categoryService;
        }

        public async Task<object> CreateTaskAsync(string title, string? description, DateTime? deadline, int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null){
                return "Category not found";
            }

            var newTask = new Models.Task{
                Title = title,
                Description = description,
                Deadline = deadline,
                Category = category
            };
            category.Tasks.Add(newTask);
            _dbContext.Update(category);

            var validationErrors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(newTask, new ValidationContext(newTask, serviceProvider: null, items: null), validationErrors, true)){
                return validationErrors;
            }

            await _dbContext.AddAsync(newTask);
            await _dbContext.SaveChangesAsync();
            return newTask;
        }

        public async Task<Models.Task> DeleteTaskByIdAsync(int taskId)
        {
            var task = await GetTaskByIdAsync(taskId);
            if (task == null)
                return null!;
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Models.Task?> GetTaskByIdAsync(int taskId)
        {
            return await _dbContext.Tasks.Include(task => task.Category).SingleOrDefaultAsync(task => task.Id.Equals(taskId));
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Categories.Where(category => category.Id.Equals(categoryId)).SelectMany(category => category.Tasks).ToListAsync();
        }

        public async Task<object> UpdateTaskAsync(int taskId, string title, string? description, DateTime? deadline, int categoryId)
        {
            var taskToUpdate = await GetTaskByIdAsync(taskId);
            if (taskToUpdate == null)
                return null!;
            if(taskToUpdate.Category.Id != categoryId){
                var categoryToSwap = await _categoryService.GetCategoryByIdAsync(categoryId);
                if(categoryToSwap == null)
                    return null!;
                categoryToSwap.Tasks.Add(taskToUpdate);
                taskToUpdate.Category.Tasks.Remove(taskToUpdate);
                _dbContext.Update(categoryToSwap);
                _dbContext.Update(taskToUpdate.Category);
            }
            taskToUpdate.Title = title;
            taskToUpdate.Description = description;
            taskToUpdate.Deadline = deadline;

            var validationErrors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(taskToUpdate, new ValidationContext(taskToUpdate, serviceProvider: null, items: null), validationErrors, true)){
                return validationErrors;
            }

            _dbContext.Update(taskToUpdate);
            await _dbContext.SaveChangesAsync();
            return taskToUpdate;
        }
    }
}