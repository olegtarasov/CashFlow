namespace ZenMoneyPlus.Models;

public record ReceiptItemModel
{
    public decimal? Sum { get; init; }
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public decimal? Quantity { get; init; }
}