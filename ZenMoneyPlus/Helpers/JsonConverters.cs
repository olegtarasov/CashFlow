using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime;

namespace ZenMoneyPlus.Helpers;

internal class UnixSecondsToInstantConverter : JsonConverter<Instant>
{
    public override Instant Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Instant.FromUnixTimeSeconds(reader.GetInt64());
    }

    public override void Write(Utf8JsonWriter writer, Instant value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.ToUnixTimeSeconds());
    }
}

internal class IncompleteTimeToLocalTimeConverter : JsonConverter<LocalTime>
{
    public override LocalTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrEmpty(value))
            return new LocalTime();

        var parts = value.Split(':', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2 || parts.Length > 3)
            return new LocalTime();

        return parts.Length == 2
                   ? new LocalTime(int.Parse(parts[0]), int.Parse(parts[1]))
                   : new LocalTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }

    public override void Write(Utf8JsonWriter writer, LocalTime value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}