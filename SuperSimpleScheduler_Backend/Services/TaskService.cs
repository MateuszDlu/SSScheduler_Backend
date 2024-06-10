using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public class TaskService : ITaskService
    {
        private readonly SchedulerDbContext _dbContext;
        public TaskService(SchedulerDbContext dbContext){
            _dbContext = dbContext;
        }

        public async Task<object> CreateTaskAsync(string title, string? description, DateTime? deadline, Category category)
        {
            var newTask = new Models.Task{
                Title = title,
                Description = description,
                Deadline = deadline,
                Category = category
            };

            //TODO validation?

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
            return await _dbContext.Tasks.SingleOrDefaultAsync(task => task.Id.Equals(taskId));
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Categories.Where(category => category.Id.Equals(categoryId)).SelectMany(category => category.Tasks).ToListAsync();
        }

        public async Task<object> UpdateTaskAsync(int taskId, string title, string? description, DateTime? deadline, Category category)
        {
            var taskToUpdate = await GetTaskByIdAsync(taskId);
            if (taskToUpdate == null)
                return null!;
            taskToUpdate.Title = title;
            taskToUpdate.Description = description;
            taskToUpdate.Deadline = deadline;
            taskToUpdate.Category = category;
            _dbContext.Update(taskToUpdate);
            await _dbContext.SaveChangesAsync();
            return taskToUpdate;
        }
    }
}