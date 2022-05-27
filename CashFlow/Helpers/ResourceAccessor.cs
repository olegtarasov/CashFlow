using System.Reflection;
using System.Text;

namespace CashFlow.Helpers;

/// <summary>
/// A helper class to load resources from an assembly.
/// </summary>
internal class ResourceAccessor
{
    private readonly Assembly _assembly;
    private readonly string _assemblyName;

    /// <summary>
    /// Creates a resource accessor for the specified assembly.
    /// </summary>
    public ResourceAccessor(Assembly assembly)
    {
        _assembly = assembly;
        _assemblyName = _assembly.GetName().Name ?? throw new InvalidOperationException("Failed to get assembly name!");
    }

    /// <summary>
    /// Gets a resource with specified name as an array of bytes.
    /// </summary>
    /// <param name="name">Resource name with folders separated by dots.</param>
    /// <exception cref="InvalidOperationException">
    /// When resource is not found.
    /// </exception>
    public byte[] Binary(string name)
    {
        using (var stream = new MemoryStream())
        {
            var resource = _assembly.GetManifestResourceStream(GetName(name));
            if (resource == null)
                throw new InvalidOperationException($"Resource not available: {name}");

            resource.CopyTo(stream);

            return stream.ToArray();
        }
    }

    /// <summary>
    /// Gets a resource with specified name as a string.
    /// </summary>
    /// <param name="name">Resource name with folders separated by dots.</param>
    /// <exception cref="InvalidOperationException">
    /// When resource is not found.
    /// </exception>
    public string String(string name)
    {
        using (var stream = new MemoryStream())
        {
            var resource = _assembly.GetManifestResourceStream(GetName(name));
            if (resource == null)
                throw new InvalidOperationException($"Resource not available: {name}");

            resource.CopyTo(stream);

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    private string GetName(string name)
    {
        return name.StartsWith(_assemblyName) ? name : $"{_assemblyName}.{name}";
    }
}