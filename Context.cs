using Microsoft.EntityFrameworkCore;

namespace Project
{
    public class Context : DbContext
    {
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Producer> Producers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=pharmacy.db");
        }
    }
}
