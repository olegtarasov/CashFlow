using System.Text.Json.Serialization;
using NodaTime;
using ZenMoneyPlus.Helpers;

namespace ZenMoneyPlus.Models;

internal record ReceiptModel
{
    public decimal? Sum { get; init; }
    public LocalDate Date { get; init; }

    [JsonConverter(typeof(IncompleteTimeToLocalTimeConverter))]
    public LocalTime Time { get; init; }

    public string? Payee { get; init; }
    public string? Address { get; init; }
    public decimal? CardSum { get; init; }
    public decimal? CashSum { get; init; }
    public string? Inn { get; init; }

    public ReceiptItemModel[] Items { get; init; } = Array.Empty<ReceiptItemModel>();
}