using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Data.Entities;

namespace ZenMoneyPlus.Web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ZenContext _context;

    public TagsController(ZenContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<Tag[]> GetTags(GetTagsMode mode)
    {
        var query = _context.Tags.Where(x => x.ParentTag == null);

        if (mode == GetTagsMode.All)
        {
            return await query.Include(x => x.ChildrenTags).ToArrayAsync();
        }

        return await query
                     .Where(mode == GetTagsMode.Income ? tag => tag.ShowIncome : tag => tag.ShowOutcome)
                     .Include(x => x.ChildrenTags.Where(mode == GetTagsMode.Income
                                                            ? tag => tag.ShowIncome
                                                            : tag => tag.ShowOutcome))
                     .ToArrayAsync();
    }

    public enum GetTagsMode
    {
        All,
        Income,
        Outcome
    }
}