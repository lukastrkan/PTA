using Microsoft.EntityFrameworkCore;
using PTA.Models;

namespace PTA
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Image> Images { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=database.db;");
        }
    }
}
