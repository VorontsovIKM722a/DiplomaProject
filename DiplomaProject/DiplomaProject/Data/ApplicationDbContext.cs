using DiplomaProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TabItemEntity> Tabs { get; set; }
        public DbSet<TestStateEntity> TestStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TabItemEntity>()
                .HasOne(t => t.TestState)
                .WithOne(x => x.TabItem)
                .HasForeignKey<TestStateEntity>(x => x.TabItemId);
        }
    }
}