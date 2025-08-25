using Microsoft.EntityFrameworkCore;
using AluguelMotos.Domain.Entities;


namespace AluguelMotos.Domain.Persistence
{
    public class AluguelMotosDbContext : DbContext
    {
        public AluguelMotosDbContext(DbContextOptions<AluguelMotosDbContext> options) : base(options) { }

        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorcycle>()
                .HasIndex(m => m.Plate)
                .IsUnique();

            modelBuilder.Entity<Courier>()
                .HasIndex(c => c.Cnpj)
                .IsUnique();
            modelBuilder.Entity<Courier>()
                .HasIndex(c => c.CnhNumber)
                .IsUnique();
        }
    }
}
