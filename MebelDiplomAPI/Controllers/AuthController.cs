using MebelDiplomAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MebelDiplomAPI.DTOs;
using MebelDiplomAPI.Services;

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

    // POST api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest(new { message = "Email уже занят" });

        var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");
        if (userRole == null)
            return BadRequest(new { message = "Роль 'User' не найдена. Перезапусти API — роли создадутся автоматически." });

        var user = new User
        {
            FullName    = dto.FullName,
            Email       = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            PhoneNumber = dto.PhoneNumber,
            RoleId      = userRole.RoleId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Сразу выдаём токен — чтобы после регистрации не нужен был отдельный логин
        var token = _jwtService.GenerateToken(user, userRole.RoleName);

        return Ok(new
        {
            token,
            user = new
            {
                userId      = user.UserId,
                fullName    = user.FullName,
                email       = user.Email,
                phoneNumber = user.PhoneNumber,
                roleId      = user.RoleId
            }
        });
    }

    // POST api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null)
            return Unauthorized(new { message = "Неверный email или пароль" });

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Неверный email или пароль" });

        if (user.IsBlocked)
            return Forbid();

        var token = _jwtService.GenerateToken(user, user.Role.RoleName);

        return Ok(new
        {
            token,
            user = new
            {
                userId      = user.UserId,
                fullName    = user.FullName,
                email       = user.Email,
                phoneNumber = user.PhoneNumber,
                roleId      = user.RoleId
            }
        });
    }
}
