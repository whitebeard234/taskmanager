using Microsoft.EntityFrameworkCore;
using taskmanager.api.Models;

namespace taskmanager.api.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public DbSet<Models.TaskItem> TaskItems => Set<Models.TaskItem>();

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
