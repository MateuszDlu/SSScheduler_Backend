using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSimpleScheduler_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SuperSimpleScheduler_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly SchedulerDbContext _dbContext;
        public UserService(SchedulerDbContext dbContext){
            _dbContext = dbContext;
        }

        public async Task<object> CreateUserAsync(string email, string password) 
        {
            var userFromDb = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (userFromDb != null){
                return "email must be unique";
            }

            //TODO password hashing
            var newUser = new User{
                Id = null,
                Email = email,
                Password = password,
                Categories = new List<Category>()
            };

            //TODO validation?
            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteUserByIdAsync(int userId) //TODO callapse delete
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
                return null!;
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string userEmail)
        {
            return await _dbContext.Users.Include(user => user.Categories).SingleOrDefaultAsync(user => user.Email.Equals(userEmail));
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.Include(user => user.Categories).SingleOrDefaultAsync(user => user.Id.Equals(userId));
        }

        public async Task<object> UpdateUserByIdAsync(int userId, string oldPassword, string newPassword) // practically just password change
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null || oldPassword != user.Password) //TODO password hashing
                return null!;
            user.Password = newPassword; //TODO password hashing
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}