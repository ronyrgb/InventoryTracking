using Backend.Data;
using Backend.Models;
using System.Linq;

namespace Backend.Data.Seeders
{
    public static class ProductSeeder
    {
        public static void Seed(AppDbContext db)
        {
            // Só insere se não houver produtos
            if (db.Products.Any())
                return;

            db.Products.AddRange(
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Notebook",
                    Code = "PROD001",
                    IsAvailable = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mouse",
                    Code = "PROD002",
                    IsAvailable = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Teclado",
                    Code = "PROD003",
                    IsAvailable = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow
                }
            );

            db.SaveChanges();
        }
    }
}
