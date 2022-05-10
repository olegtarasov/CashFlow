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
    public DbSet<TransactionTag> TransactionTags { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<ReceiptItem> ReceiptItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>()
                    .HasOne<Tag>(x => x.ParentTag)
                    .WithMany(x => x.ChildrenTags)
                    .HasForeignKey(x => x.Parent);
            
        modelBuilder.Entity<TransactionTag>()
                    .HasKey(x => new {x.TagId, x.TransactionId});

        modelBuilder.Entity<ReceiptItem>()
                    .HasOne(x => x.Receipt)
                    .WithMany(x => x.ReceiptItems)
                    .HasForeignKey(x => x.ReceiptId);

        modelBuilder.Entity<Receipt>()
                    .HasOne(x => x.Transaction)
                    .WithOne(x => x.Receipt)
                    .HasForeignKey<Receipt>(x => x.TransactionId);

        // modelBuilder.Entity<TransactionTag>()
        //     .HasOne(tt => tt.Tag)
        //     .WithMany(t => t.TransactionTags)
        //     .HasForeignKey()

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);

        options
            .UseLazyLoadingProxies()
            .UseSqlite("Data Source=data.db");
    }
}