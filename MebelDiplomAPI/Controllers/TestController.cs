using Microsoft.AspNetCore.Mvc;
using MebelDiplomAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly MebelDiplomContext _context;

    public TestController(MebelDiplomContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var categoriesCount = _context.Categories.Count();
        return Ok($"Категорий в БД: {categoriesCount}");
    }
}