using Backend.Data;
using Backend.Models;
using BCrypt.Net;

namespace Backend.Data.Seeders
{
    public static class UserSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Users.Any())
                return; // Já existe usuário, não faz nada

            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@teste.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    CreatedAt = DateTime.UtcNow,
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "user1",
                    Email = "user1@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    CreatedAt = DateTime.UtcNow,
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "user2",
                    Email = "user2@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    CreatedAt = DateTime.UtcNow,
                }
            };

            db.Users.AddRange(users);
            db.SaveChanges();
        }
    }
}
