namespace ZenMoneyPlus.Data.Entities;

/// <summary>
/// Receipt item.
/// </summary>
public class ReceiptItem
{
    /// <summary>
    /// Item id.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Total paid (usually <see cref="Price"/> * <see cref="Quantity"/>).
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Item name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Item price for a single unit.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Item quantity.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Parent receipt id.
    /// </summary>
    public long ReceiptId { get; set; }

    /// <summary>
    /// Parent receipt.
    /// </summary>
    public virtual Receipt? Receipt { get; set; }
}