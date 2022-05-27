using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CashFlow.Data;

/// <summary>
/// Used for migrations only. 
/// </summary>
public class ZenContextFactory : IDesignTimeDbContextFactory<ZenContext>
{
    /// <inheritdoc />
    public ZenContext CreateDbContext(string[] args)
    {
        return new ZenContext();
    }
}

/// <summary>
/// Used to add a pragma at the top of each migration in order
/// to mute compiler warnings about XML comments.
/// </summary>
public class CustomCSharpMigrationsGenerator : CSharpMigrationsGenerator
{
    /// <summary>
    /// Ctor.
    /// </summary>
    public CustomCSharpMigrationsGenerator(
        MigrationsCodeGeneratorDependencies dependencies,
        CSharpMigrationsGeneratorDependencies csharpDependencies)
        : base(dependencies, csharpDependencies)
    {
    }

    /// <inheritdoc />
    public override string GenerateMigration(
        string? migrationNamespace,
        string migrationName,
        IReadOnlyList<MigrationOperation> upOperations,
        IReadOnlyList<MigrationOperation> downOperations)
        => @"#pragma warning disable CS1591" +
           Environment.NewLine +
           base.GenerateMigration(migrationNamespace, migrationName, upOperations, downOperations) +
           Environment.NewLine +
           @"#pragma warning restore CS1591";

    /// <inheritdoc />
    public override string GenerateSnapshot(
        string? modelSnapshotNamespace,
        Type contextType,
        string modelSnapshotName,
        IModel model) =>
        @"#pragma warning disable CS1591" +
        Environment.NewLine +
        base.GenerateSnapshot(modelSnapshotNamespace, contextType, modelSnapshotName, model) +
        Environment.NewLine +
        @"#pragma warning restore CS1591";

    /// <inheritdoc />
    public override string GenerateMetadata(
        string? migrationNamespace,
        Type contextType,
        string migrationName,
        string migrationId,
        IModel targetModel) =>
        @"#pragma warning disable CS1591" +
        Environment.NewLine +
        base.GenerateMetadata(migrationNamespace, contextType, migrationName, migrationId, targetModel) +
        Environment.NewLine +
        @"#pragma warning restore CS1591";
}

/// <summary>
/// We need this for migrations only.
/// </summary>
public class CustomDesignTimeServices : IDesignTimeServices
{
    /// <inheritdoc />
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        => serviceCollection.AddSingleton<IMigrationsCodeGenerator, CustomCSharpMigrationsGenerator>();
}