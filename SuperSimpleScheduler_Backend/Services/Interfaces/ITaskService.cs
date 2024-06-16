using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public interface ITaskService
    {
        public Task<IEnumerable<Models.Task>> GetTasksByCategoryIdAsync(int categoryId);
        public Task<Models.Task> GetTaskByIdAsync(int taskId);
        public Task<Models.Task> DeleteTaskByIdAsync(int taskId);
        public Task<Object> CreateTaskAsync(string title, string? description, DateTime? deadline, int categoryId);
        public Task<Object> UpdateTaskAsync(int taskId, string title, string? description, DateTime? deadline, int categoryId);
    }
}