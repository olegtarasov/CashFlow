﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZenMoneyPlus.Data;

#nullable disable

namespace ZenMoneyPlus.Migrations
{
    [DbContext(typeof(ZenContext))]
    [Migration("20220510135108_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Receipt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("CardSum")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("CashSum")
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Payee")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Sum")
                        .HasColumnType("TEXT");

                    b.Property<string>("Time")
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

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.ReceiptItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Quantity")
                        .HasColumnType("TEXT");

                    b.Property<long>("ReceiptId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Sum")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptItems");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Setting", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("BudgetIncome")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("BudgetOutcome")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Changed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .HasColumnType("TEXT");

                    b.Property<string>("Parent")
                        .HasColumnType("TEXT");

                    b.Property<string>("Picture")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowIncome")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowOutcome")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<long>("User")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Parent");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<long>("Changed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<long>("Created")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("GetReceiptFailed")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Hold")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Income")
                        .HasColumnType("TEXT");

                    b.Property<string>("IncomeAccount")
                        .HasColumnType("TEXT");

                    b.Property<string>("IncomeBankId")
                        .HasColumnType("TEXT");

                    b.Property<long>("IncomeInstrument")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("TEXT");

                    b.Property<string>("Merchant")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("OpIncome")
                        .HasColumnType("TEXT");

                    b.Property<string>("OpIncomeInstrument")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("OpOutcome")
                        .HasColumnType("TEXT");

                    b.Property<string>("OpOutcomeInstrument")
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalPayee")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Outcome")
                        .HasColumnType("TEXT");

                    b.Property<string>("OutcomeAccount")
                        .HasColumnType("TEXT");

                    b.Property<string>("OutcomeBankId")
                        .HasColumnType("TEXT");

                    b.Property<long>("OutcomeInstrument")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Payee")
                        .HasColumnType("TEXT");

                    b.Property<string>("QrCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReminderMarker")
                        .HasColumnType("TEXT");

                    b.Property<long>("User")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Viewed")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.TransactionTag", b =>
                {
                    b.Property<string>("TagId")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionId")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("TagId", "TransactionId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionTags");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Receipt", b =>
                {
                    b.HasOne("ZenMoneyPlus.Data.Entities.Transaction", "Transaction")
                        .WithOne("Receipt")
                        .HasForeignKey("ZenMoneyPlus.Data.Entities.Receipt", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.ReceiptItem", b =>
                {
                    b.HasOne("ZenMoneyPlus.Data.Entities.Receipt", "Receipt")
                        .WithMany("ReceiptItems")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Tag", b =>
                {
                    b.HasOne("ZenMoneyPlus.Data.Entities.Tag", "ParentTag")
                        .WithMany("ChildrenTags")
                        .HasForeignKey("Parent");

                    b.Navigation("ParentTag");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.TransactionTag", b =>
                {
                    b.HasOne("ZenMoneyPlus.Data.Entities.Tag", "Tag")
                        .WithMany("TransactionTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZenMoneyPlus.Data.Entities.Transaction", "Transaction")
                        .WithMany("TransactionTags")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Receipt", b =>
                {
                    b.Navigation("ReceiptItems");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Tag", b =>
                {
                    b.Navigation("ChildrenTags");

                    b.Navigation("TransactionTags");
                });

            modelBuilder.Entity("ZenMoneyPlus.Data.Entities.Transaction", b =>
                {
                    b.Navigation("Receipt");

                    b.Navigation("TransactionTags");
                });
#pragma warning restore 612, 618
        }
    }
}