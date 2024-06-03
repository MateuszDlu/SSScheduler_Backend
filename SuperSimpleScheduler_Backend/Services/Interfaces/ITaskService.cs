using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public interface ITaskService
    {
        public Task<IEnumerable<Models.Task>> GetTasksByCategoryId(int categoryId);
        public Task<Models.Task> GetTaskById(int taskId);
        public Task<Models.Task> DeleteTaskById(int taskId);
        public Task<Object> CreateTask(string title, string? description, DateTime? deadline, Category category);
        public Task<Object> UpdateTask(int taskId, string title, string? description, DateTime? deadline, Category category);
    }
}