using Microsoft.AspNetCore.Mvc;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Web.Models;

namespace ZenMoneyPlus.Web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MonthlyController : ControllerBase
{
    private readonly ZenContext _context;

    public MonthlyController(ZenContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<MontlyResponse> GetMonthlyData(MontlyRequest request)
    {
        return new();
    }
}