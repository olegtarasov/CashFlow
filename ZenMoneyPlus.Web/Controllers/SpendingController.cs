using Microsoft.AspNetCore.Mvc;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Web.Models;

namespace ZenMoneyPlus.Web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SpendingController : ControllerBase
{
    private readonly ZenContext _context;

    public SpendingController(ZenContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<MontlyResponse> GetSpendingData(MontlyRequest request)
    {
        return new();
    }
}