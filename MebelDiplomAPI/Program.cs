using MebelDiplomAPI.Models;
using MebelDiplomAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MebelDiplomContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<JwtService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ╔══════════════════════════════════════════════╗
// ║  CORS — разрешаем фронту обращаться к API   ║
// ╚══════════════════════════════════════════════╝
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7011",
                "http://localhost:7011",
                "http://localhost:5173",   // Vite по умолчанию
                "https://localhost:5173",
                "http://localhost:5174",   // Vite запасной порт
                "http://localhost:3000"    // на всякий случай
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MebelDiplomAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Введи токен в формате: Bearer eyJhbGciOi...",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// === PIPELINE ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ⚠️ CORS должен быть ДО Authentication/Authorization
app.UseCors("AllowFrontend");

// пока убери из за него много проблем в апи у меня :( app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// === Сидинг ролей (если ещё нет) ===
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MebelDiplomContext>();

    // Создаём роли если нет
    if (!await context.Roles.AnyAsync())
    {
        context.Roles.AddRange(
            new Role { RoleName = "Admin" },
            new Role { RoleName = "User" }
        );
        await context.SaveChangesAsync();
        Console.WriteLine("Роли созданы: Admin, User");
    }

    // Создаём админа если нет
    if (!await context.Users.AnyAsync(u => u.Email == "admin@mail.ru"))
    {
        var adminRole = await context.Roles.FirstAsync(r => r.RoleName == "Admin");
        var admin = new User
        {
            FullName = "Admin",
            Email = "admin@mail.ru",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Zxc2281337"),
            RoleId = adminRole.RoleId
        };
        context.Users.Add(admin);
        await context.SaveChangesAsync();
        Console.WriteLine("Админ создан: admin@mail.ru / Zxc2281337");
    }
}

app.Run();
