using Microsoft.EntityFrameworkCore;

namespace Sep2026.FootballWorld.Data
{
    public class FootballDbContext : DbContext
    {
             

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-6BI45GA\\SQLEXPRESS;Database=FootballWorld;Trusted_Connection=True;TrustServerCertificate=True"
                );
            }
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}