using Backend.Data;
using Backend.Data.Seeders;
using Backend.Repositories;
using Backend.Services;
using Backend.Services.Interfaces;
using Backend.Validators;
using FluentValidation;
using FluentValidation.AspNetCore; // necessário para AddFluentValidation
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==============================
// Configurações JWT
// ==============================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ==============================
// Services e Repositories
// ==============================
builder.Services.AddRepositories();
builder.Services.AddServices(); // ServiceBuilder

// ==============================
// Validators
// ==============================
// Registra todos os validators e ativa validação automática

builder.Services.AddFluentValidationAutoValidation();      
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidators(); // 👈 CORREÇÃO

// ==============================
// Controllers
// ==============================
builder.Services.AddControllers();

// ==============================
// DbContext
// ==============================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ==============================
// Swagger
// ==============================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==============================
// CORS
// ==============================
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// ==============================
// Pipeline
// ==============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ==============================
// Migrations + Seeders
// ==============================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    
    db.Database.Migrate(); // aplica migrations

    if (!db.Users.Any())
        UserSeeder.Seed(db);

    if (!db.Products.Any())
        ProductSeeder.Seed(db);
}

app.Run();
