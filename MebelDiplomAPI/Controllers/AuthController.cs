using MebelDiplomAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MebelDiplomAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly MebelDiplomContext _context;
    private readonly JwtService _jwtService;

    public AuthController(MebelDiplomContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email уже существует");

        var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");
        if (userRole == null)
            return BadRequest("Роль 'User' не найдена в базе. Сначала запусти сидинг ролей.");

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RoleId = userRole.RoleId   // теперь безопасно
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Регистрация успешна", userId = user.UserId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null)
            return Unauthorized("Неверный email");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Неверный пароль");

        var token = _jwtService.GenerateToken(user, user.Role.RoleName);

        return Ok(new { token });
    }
}