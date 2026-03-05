using MebelDiplomAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]   // только админ
    public class AdminController : ControllerBase
    {
        private readonly MebelDiplomContext _context;

        public AdminController(MebelDiplomContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.UserId,
                    u.FullName,
                    u.Email,
                    u.RoleId,
                    u.IsBlocked
                })
                .ToListAsync();

            return Ok(users);
        }

        // Можно потом добавить удаление, блокировку и т.д.
    }
}