using Microsoft.EntityFrameworkCore;
using ZenMoneyPlus.Data.Entities;

namespace ZenMoneyPlus.Data;

public class ZenContext : DbContext
{
    public ZenContext()
    {
    }

    public ZenContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<ReceiptItem> ReceiptItems { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);

        options
            .UseLazyLoadingProxies()
            .UseSqlite("Data Source=data.db", x => x.UseNodaTime());
    }
}