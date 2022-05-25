using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace ZenMoneyPlus.Web.Models;

public enum SpendingMode
{
    Month,
    Year
}

public record MonthAndYear(int Month, int Year);

public record MonthRange(MonthAndYear From, MonthAndYear To);

public record YearRange(int From, int To);

public record SpendingRequest(
    [Required] SpendingMode Mode,
    MonthRange? MonthRange,
    YearRange? YearRange,
    [Required] string[] Tags);

public record SpendingSerie(string Name, decimal[] Data);

public record SpendingResponse(string[] Categories, SpendingSerie[] Series);