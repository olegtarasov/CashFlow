using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Data.Entities;

namespace ZenMoneyPlus.Web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ZenContext _context;

    public CategoriesController(ZenContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<Tag[]> GetCategories()
    {
        var roots = await _context.Tags
                                  .Include(x => x.ChildrenTags)
                                  .Where(x => x.ParentTag == null)
                                  .ToArrayAsync();

        return roots;
    }
}