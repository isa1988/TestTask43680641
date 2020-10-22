using Microsoft.EntityFrameworkCore;
using TestTask.Core.DataBase;
using TestTask.DAL.DataBase.Configuration;

namespace TestTask.DAL.DataBase
{
    public class DbContextTestTask : DbContext
    {
        public DbContextTestTask(DbContextOptions<DbContextTestTask> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
