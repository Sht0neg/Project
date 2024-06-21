using Microsoft.EntityFrameworkCore;

namespace Project
{
    public class Context : DbContext
    {
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Producer> Producers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=12345;database=pharmacy", new MySqlServerVersion(new Version(8, 4, 0)));
        }
    }
}
