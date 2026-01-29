using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {
            Database.EnsureCreated(); // Cria o banco se não existir
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductLoan> ProductLoans { get; set; } = null!;
        public DbSet<AccessLog> AccessLogs { get; set; } = null!;
        public DbSet<AccessLog> ActionLog { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductLoan>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.ProductLoans)
                .HasForeignKey(pl => pl.UserId);

            modelBuilder.Entity<ProductLoan>()
                .HasOne(pl => pl.Product)
                .WithMany(p => p.ProductLoans)
                .HasForeignKey(pl => pl.ProductId);
        }
    }
}
