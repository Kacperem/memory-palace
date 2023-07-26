using Microsoft.EntityFrameworkCore;

namespace MemoryPalaceAPI.Entities
{
    public class MemoryPalaceDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=MemoryPalaceDb;Trusted_Connection=True;";
        public DbSet<TwoDigitElement> TwoDigitElements { get; set; }
        public DbSet<TwoDigitSystem> twoDigitSystems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TwoDigitElement>()
                .Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(2);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
