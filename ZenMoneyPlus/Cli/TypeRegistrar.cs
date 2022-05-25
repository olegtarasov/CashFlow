using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace ZenMoneyPlus.Cli;

/// <summary>
/// A type registrar to use with Spectre.Console projects.
/// </summary>
internal sealed class TypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection _builder;

    /// <summary>
    /// Ctor.
    /// </summary>
    public TypeRegistrar(IServiceCollection builder)
    {
        _builder = builder;
    }

    /// <inheritdoc />
    public ITypeResolver Build()
    {
        return new TypeResolver(_builder.BuildServiceProvider());
    }

    /// <inheritdoc />
    public void Register(Type service, Type implementation)
    {
        _builder.AddSingleton(service, implementation);
    }

    /// <inheritdoc />
    public void RegisterInstance(Type service, object implementation)
    {
        _builder.AddSingleton(service, implementation);
    }

    /// <inheritdoc />
    public void RegisterLazy(Type service, Func<object> func)
    {
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        _builder.AddSingleton(service, provider => func());
    }
}