using Microsoft.EntityFrameworkCore;

namespace MemoryPalaceAPI.Entities
{
    public class MemoryPalaceDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=MemoryPalaceDb;Trusted_Connection=True;";
        public DbSet<TwoDigitElement> TwoDigitElements { get; set; }
        public DbSet<TwoDigitSystem> TwoDigitSystems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TwoDigitElement>()
                .Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
