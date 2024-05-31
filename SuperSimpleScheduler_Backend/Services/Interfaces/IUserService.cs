using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;

namespace SuperSimpleScheduler_Backend.Services
{
    public interface IUserService
    {
        public Task<User> GetUserById(int userId);
        public Task<User> GetUserByEmail(string userEmail);
        public Task<User> DeleteUserById(int userId);
        public Task<Object> UpdateUserById(int userId, string userEmail, string password);
        public Task<Object> CreateUser(string email, string password);
        public Task<User> GetAllUsers();
    }
}