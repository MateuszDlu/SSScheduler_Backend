using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public class TaskService : ITaskService
    {
        public Task<object> CreateTask(string title, string? description, DateTime? deadline, Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Task> DeleteTaskById(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Task> GetTaskById(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Task>> GetTasksByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateTask(int taskId, string title, string? description, DateTime? deadline, Category category)
        {
            throw new NotImplementedException();
        }
    }
}