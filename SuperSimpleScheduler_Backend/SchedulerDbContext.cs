using System;
using System.Collections.Generic;
using System.Linq;
using SuperSimpleScheduler_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SuperSimpleScheduler_Backend
{
    public class SchedulerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        
        public SchedulerDbContext(DbContextOptions<SchedulerDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your model here
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SSSchedulerDb"));
            }
        }
    }
}
