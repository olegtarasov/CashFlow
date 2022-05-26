using Microsoft.EntityFrameworkCore;
using ZenMoneyPlus.Data.Entities;

namespace ZenMoneyPlus.Data;

/// <summary>
/// Db context.
/// </summary>
public class ZenContext : DbContext
{
    /// <summary>
    /// Ctor.
    /// </summary>
    public ZenContext()
    {
    }

    /// <summary>
    /// Ctor.
    /// </summary>
    public ZenContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Transactions.
    /// </summary>
    public DbSet<Transaction> Transactions => Set<Transaction>();

    /// <summary>
    /// Tags.
    /// </summary>
    public DbSet<Tag> Tags => Set<Tag>();

    /// <summary>
    /// Accounts.
    /// </summary>
    public DbSet<Account> Accounts => Set<Account>();

    /// <summary>
    /// Settings.
    /// </summary>
    public DbSet<Setting> Settings => Set<Setting>();

    /// <summary>
    /// Receipts.
    /// </summary>
    public DbSet<Receipt> Receipts => Set<Receipt>();

    /// <summary>
    /// Receipt items.
    /// </summary>
    public DbSet<ReceiptItem> ReceiptItems => Set<ReceiptItem>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>()
                    .HasOne(x => x.ParentTag)
                    .WithMany(x => x.ChildrenTags)
                    .HasForeignKey(x => x.Parent);

        modelBuilder.Entity<Transaction>()
                    .HasMany(x => x.Tags)
                    .WithMany(x => x.Transactions);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);

        options
            .UseLazyLoadingProxies()
            .UseSqlite("Data Source=data.db", x => x.UseNodaTime());
    }
}