﻿// <auto-generated />
using System;
using Homebroker.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Homebroker.Migrations
{
    [DbContext(typeof(HomebrokerDbContext))]
    [Migration("20250123164332_UpdateDateTime")]
    partial class UpdateDateTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Homebroker.Domain.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Homebroker.Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssetId")
                        .HasColumnType("uuid");

                    b.Property<int>("Partial")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Shares")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("WalletId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Homebroker.Domain.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BrokerTransactionId")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("InvestorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Shares")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Homebroker.Domain.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Homebroker.Domain.WalletAsset", b =>
                {
                    b.Property<Guid>("WalletId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssetId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Shares")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("WalletId", "AssetId");

                    b.HasIndex("AssetId");

                    b.HasIndex("WalletId", "AssetId")
                        .IsUnique();

                    b.ToTable("WalletAssets");
                });

            modelBuilder.Entity("Homebroker.Domain.Order", b =>
                {
                    b.HasOne("Homebroker.Domain.Asset", "Asset")
                        .WithMany("Orders")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Homebroker.Domain.Wallet", "Wallet")
                        .WithMany("Orders")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Homebroker.Domain.Transaction", b =>
                {
                    b.HasOne("Homebroker.Domain.Order", "Order")
                        .WithMany("Transactions")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Homebroker.Domain.WalletAsset", b =>
                {
                    b.HasOne("Homebroker.Domain.Asset", "Asset")
                        .WithMany("WalletAssets")
                        .HasForeignKey("AssetId")
                        .IsRequired();

                    b.HasOne("Homebroker.Domain.Wallet", "Wallet")
                        .WithMany("WalletAssets")
                        .HasForeignKey("WalletId")
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Homebroker.Domain.Asset", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("WalletAssets");
                });

            modelBuilder.Entity("Homebroker.Domain.Order", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Homebroker.Domain.Wallet", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("WalletAssets");
                });
#pragma warning restore 612, 618
        }
    }
}
