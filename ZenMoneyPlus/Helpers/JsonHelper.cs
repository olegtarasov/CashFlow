using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace ZenMoneyPlus.Helpers;

/// <summary>
/// Helper methods for JSON serialization.
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// Default JSON serializer options.
    /// </summary>
    public static JsonSerializerOptions DefaultOptions => new JsonSerializerOptions().ConfigureDefaultOptions();

    /// <summary>
    /// Gets default serializer options.
    /// </summary>
    public static JsonSerializerOptions GetDefaultOptions() =>
        new JsonSerializerOptions().ConfigureDefaultOptions();

    /// <summary>
    /// Configures an instance of <see cref="JsonSerializerOptions"/>.
    /// </summary>
    /// <param name="options">Options to configure.</param>
    public static JsonSerializerOptions ConfigureDefaultOptions(this JsonSerializerOptions options)
    {
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        options.AllowTrailingCommas = true;
        options.PropertyNameCaseInsensitive = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        options.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

        return options;
    }
}