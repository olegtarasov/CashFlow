﻿#pragma warning disable CS1591
// <auto-generated />
using System;
using CashFlow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CashFlow.Migrations
{
    [DbContext(typeof(ZenContext))]
    [Migration("20220527100715_LearnReactFieldCrop")]
    partial class LearnReactFieldCrop
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("CashFlow.Data.Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Changed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("InBalance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Receipt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("CardSum")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("CashSum")
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Payee")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Sum")
                        .HasColumnType("TEXT");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.ReceiptItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.Property<long>("ReceiptId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Sum")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptItems");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Changed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Parent")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ShowIncome")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowOutcome")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Parent");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Changed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Created")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Income")
                        .HasColumnType("TEXT");

                    b.Property<string>("IncomeAccountId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Outcome")
                        .HasColumnType("TEXT");

                    b.Property<string>("OutcomeAccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Payee")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Viewed")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IncomeAccountId");

                    b.HasIndex("OutcomeAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TagTransaction", b =>
                {
                    b.Property<string>("TagsId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionsId")
                        .HasColumnType("TEXT");

                    b.HasKey("TagsId", "TransactionsId");

                    b.HasIndex("TransactionsId");

                    b.ToTable("TagTransaction");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Receipt", b =>
                {
                    b.HasOne("CashFlow.Data.Entities.Transaction", "Transaction")
                        .WithOne("Receipt")
                        .HasForeignKey("CashFlow.Data.Entities.Receipt", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.ReceiptItem", b =>
                {
                    b.HasOne("CashFlow.Data.Entities.Receipt", "Receipt")
                        .WithMany("Items")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Tag", b =>
                {
                    b.HasOne("CashFlow.Data.Entities.Tag", "ParentTag")
                        .WithMany("ChildrenTags")
                        .HasForeignKey("Parent");

                    b.Navigation("ParentTag");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Transaction", b =>
                {
                    b.HasOne("CashFlow.Data.Entities.Account", "IncomeAccount")
                        .WithMany()
                        .HasForeignKey("IncomeAccountId");

                    b.HasOne("CashFlow.Data.Entities.Account", "OutcomeAccount")
                        .WithMany()
                        .HasForeignKey("OutcomeAccountId");

                    b.Navigation("IncomeAccount");

                    b.Navigation("OutcomeAccount");
                });

            modelBuilder.Entity("TagTransaction", b =>
                {
                    b.HasOne("CashFlow.Data.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CashFlow.Data.Entities.Transaction", null)
                        .WithMany()
                        .HasForeignKey("TransactionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Receipt", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Tag", b =>
                {
                    b.Navigation("ChildrenTags");
                });

            modelBuilder.Entity("CashFlow.Data.Entities.Transaction", b =>
                {
                    b.Navigation("Receipt");
                });
#pragma warning restore 612, 618
        }
    }
}

#pragma warning restore CS1591