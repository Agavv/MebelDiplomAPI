using MebelDiplomAPI.DTOs;
using MebelDiplomAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MebelDiplomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MebelDiplomContext _context; 

        public UserController(MebelDiplomContext context)
        {
            _context = context;
        }

        // GET: api/user/profile
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Токен не содержит UserID");

            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("Неверный формат UserID");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Пользователь не найден");

            var profile = new ProfileDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                RoleId = user.RoleId,
                CreatedAt = user.CreatedAt,
                IsBlocked = user.IsBlocked
            };

            return Ok(profile);
        }
    }
}