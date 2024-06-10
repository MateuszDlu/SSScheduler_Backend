using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public interface IUserService
    {
        public Task<User?> GetUserByIdAsync(int userId);
        public Task<User?> GetUserByEmailAsync(string userEmail);
        public Task<User> DeleteUserByIdAsync(int userId);
        public Task<Object> UpdateUserByIdAsync(int userId, string oldPassword, string newPassword);
        public Task<Object> CreateUserAsync(string email, string password);
        public Task<IEnumerable<User>> GetAllUsersAsync();
    }
}