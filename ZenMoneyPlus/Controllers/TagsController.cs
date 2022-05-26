using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Data.Entities;

namespace ZenMoneyPlus.Controllers;

/// <summary>
/// A controller to work with tags.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ZenContext _context;

    /// <summary>
    /// Ctor.
    /// </summary>
    public TagsController(ZenContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all tags filtered by type.
    /// </summary>
    /// <param name="mode">Filter mode.</param>
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

    /// <summary>
    /// Tags filter mode.
    /// </summary>
    public enum GetTagsMode
    {
        /// <summary>
        /// All tags.
        /// </summary>
        All,

        /// <summary>
        /// Only income tags.
        /// </summary>
        Income,

        /// <summary>
        /// Only outcome tags.
        /// </summary>
        Outcome
    }
}