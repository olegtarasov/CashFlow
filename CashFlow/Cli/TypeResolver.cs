using Spectre.Console.Cli;

namespace CashFlow.Cli;

/// <summary>
/// A type resolver to use with Spectre.Console projects.
/// </summary>
internal sealed class TypeResolver : ITypeResolver, IDisposable
{
    private readonly IServiceProvider _provider;

    /// <summary>
    /// Ctor.
    /// </summary>
    public TypeResolver(IServiceProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_provider is IDisposable disposable)
            disposable.Dispose();
    }

    /// <inheritdoc />
    public object? Resolve(Type? type)
    {
        if (type == null)
            return null;

        return _provider.GetService(type);
    }
}