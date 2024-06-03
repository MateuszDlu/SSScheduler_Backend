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
            // Configure properties for User entity
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();

            // Configure properties for Category entity
            modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(100);

            // Configure properties for Task entity
            modelBuilder.Entity<Models.Task>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Models.Task>().Property(t => t.Title).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Models.Task>().Property(t => t.Description).HasMaxLength(500);
            modelBuilder.Entity<Models.Task>().Property(t => t.Deadline).HasColumnType("datetime2");

            // User-Category relationship
            modelBuilder.Entity<Category>().HasOne(c => c.User).WithMany(u => u.Categories);

            // Task-Category relationship
            modelBuilder.Entity<Models.Task>().HasOne(t => t.Category).WithMany(c => c.Tasks);
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
