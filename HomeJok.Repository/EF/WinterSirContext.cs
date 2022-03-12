using HomeJok.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeJok.Repository.EF
{
    public class WinterSirContext : DbContext
    {
        public WinterSirContext(DbContextOptions<WinterSirContext> options)
            : base(options)
        {
        }

        public DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo");
        }
    }
}
