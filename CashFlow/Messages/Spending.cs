using System.ComponentModel.DataAnnotations;

namespace CashFlow.Messages;

/// <summary>
/// Spending mode.
/// </summary>
public enum SpendingMode
{
    /// <summary>
    /// By month.
    /// </summary>
    Month,

    /// <summary>
    /// By year.
    /// </summary>
    Year
}

/// <summary>
/// Month and year.
/// </summary>
/// <param name="Month">Month.</param>
/// <param name="Year">Year.</param>
public record MonthAndYear(int Month, int Year) : IComparable<MonthAndYear>
{
    /// <inheritdoc />
    public int CompareTo(MonthAndYear? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (ReferenceEquals(null, other))
        {
            return 1;
        }

        int year = Year.CompareTo(other.Year);
        if (year != 0)
            return year;

        return Month.CompareTo(other.Month);
    }

    /// <summary>Month.</summary>
    public int Month { get; init; } = Month;

    /// <summary>Year.</summary>
    public int Year { get; init; } = Year;
}

/// <summary>
/// Month range.
/// </summary>
/// <param name="From">Range start.</param>
/// <param name="To">Range end.</param>
public record MonthRange(MonthAndYear From, MonthAndYear To)
{
    /// <summary>Range start.</summary>
    public MonthAndYear From { get; init; } = From;

    /// <summary>Range end.</summary>
    public MonthAndYear To { get; init; } = To;
}

/// <summary>
/// Year range.
/// </summary>
/// <param name="From">Range start.</param>
/// <param name="To">Range end.</param>
public record YearRange(int From, int To)
{
    /// <summary>Range start.</summary>
    public int From { get; init; } = From;

    /// <summary>Range end.</summary>
    public int To { get; init; } = To;
}

/// <summary>
/// Spending request.
/// </summary>
/// <param name="Mode">Spending mode.</param>
/// <param name="MonthRange">Month range if mode is <see cref="SpendingMode.Month"/>.</param>
/// <param name="YearRange">Year range if mode is <see cref="SpendingMode.Year"/>.</param>
/// <param name="Tags">An array of tags to filter spendings with.</param>
public record SpendingRequest(
    [Required] SpendingMode Mode,
    MonthRange? MonthRange,
    YearRange? YearRange,
    [Required] string[] Tags)
{
    /// <summary>Spending mode.</summary>
    public SpendingMode Mode { get; init; } = Mode;

    /// <summary>Month range if mode is <see cref="SpendingMode.Month"/>.</summary>
    public MonthRange? MonthRange { get; init; } = MonthRange;

    /// <summary>Year range if mode is <see cref="SpendingMode.Year"/>.</summary>
    public YearRange? YearRange { get; init; } = YearRange;

    /// <summary>An array of tags to filter spendings with.</summary>
    public string[] Tags { get; init; } = Tags;
}

/// <summary>
/// Spending serie.
/// </summary>
/// <param name="Name">Transaction tag title.</param>
/// <param name="Data">
/// Total spending amounts for this tag for each consecutive period. Number of elements in <paramref name="Data"/>
/// is always the same as number of items in <see cref="SpendingResponse.Categories"/>.
/// </param>
public record SpendingSerie(string Name, decimal[] Data)
{
    /// <summary>Transaction tag title.</summary>
    public string Name { get; init; } = Name;

    /// <summary>
    /// Total spending amounts for this tag for each consecutive period. Number of elements in <see cref="Data"/>
    /// is always the same as number of items in <see cref="SpendingResponse.Categories"/>.
    /// </summary>
    public decimal[] Data { get; init; } = Data;
}

/// <summary>
/// Spending response.
/// </summary>
/// <param name="Categories">Individual months or years depending on <see cref="SpendingMode"/>.</param>
/// <param name="Series">Series.</param>
public record SpendingResponse(string[] Categories, SpendingSerie[] Series)
{
    /// <summary>Individual months or years depending on <see cref="SpendingMode"/>.</summary>
    public string[] Categories { get; init; } = Categories;

    /// <summary>Series.</summary>
    public SpendingSerie[] Series { get; init; } = Series;
}