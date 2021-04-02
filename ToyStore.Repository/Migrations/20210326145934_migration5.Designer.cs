﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToyStore.Repository.Models;

namespace ToyStore.Repository.Migrations
{
    [DbContext(typeof(DbContextClass))]
    [Migration("20210326145934_migration5")]
    partial class migration5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SellableTag", b =>
                {
                    b.Property<Guid>("TagSellablesSellableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagsTagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagSellablesSellableId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("SellableTag");
                });

            modelBuilder.Entity("ToyStore.Models.Abstracts.Sellable", b =>
                {
                    b.Property<Guid>("SellableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CurrentOfferSellableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SellableDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellableImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellableName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SellablePrice")
                        .HasColumnType("float");

                    b.HasKey("SellableId");

                    b.HasIndex("CurrentOfferSellableId");

                    b.ToTable("Sellables");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CurrentOrderOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerPass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerUName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CurrentOrderOrderId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OrderLocationLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderedByCustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderLocationLocationId");

                    b.HasIndex("OrderedByCustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.SellableStack", b =>
                {
                    b.Property<Guid>("SellableStackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid?>("ItemSellableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SellableStackId");

                    b.HasIndex("ItemSellableId");

                    b.HasIndex("LocationId");

                    b.HasIndex("OrderId");

                    b.ToTable("SellableStacks");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TagId");

                    b.HasIndex("TagName")
                        .IsUnique()
                        .HasFilter("[TagName] IS NOT NULL");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SellableTag", b =>
                {
                    b.HasOne("ToyStore.Models.Abstracts.Sellable", null)
                        .WithMany()
                        .HasForeignKey("TagSellablesSellableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToyStore.Models.DBModels.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToyStore.Models.Abstracts.Sellable", b =>
                {
                    b.HasOne("ToyStore.Models.Abstracts.Sellable", "CurrentOffer")
                        .WithMany("Products")
                        .HasForeignKey("CurrentOfferSellableId");

                    b.Navigation("CurrentOffer");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Customer", b =>
                {
                    b.HasOne("ToyStore.Models.DBModels.Order", "CurrentOrder")
                        .WithMany()
                        .HasForeignKey("CurrentOrderOrderId");

                    b.Navigation("CurrentOrder");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Order", b =>
                {
                    b.HasOne("ToyStore.Models.DBModels.Location", "OrderLocation")
                        .WithMany()
                        .HasForeignKey("OrderLocationLocationId");

                    b.HasOne("ToyStore.Models.DBModels.Customer", "OrderedBy")
                        .WithMany("FinishedOrders")
                        .HasForeignKey("OrderedByCustomerId");

                    b.Navigation("OrderedBy");

                    b.Navigation("OrderLocation");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.SellableStack", b =>
                {
                    b.HasOne("ToyStore.Models.Abstracts.Sellable", "Item")
                        .WithMany()
                        .HasForeignKey("ItemSellableId");

                    b.HasOne("ToyStore.Models.DBModels.Location", null)
                        .WithMany("LocationInventory")
                        .HasForeignKey("LocationId");

                    b.HasOne("ToyStore.Models.DBModels.Order", null)
                        .WithMany("cart")
                        .HasForeignKey("OrderId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ToyStore.Models.Abstracts.Sellable", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Customer", b =>
                {
                    b.Navigation("FinishedOrders");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Location", b =>
                {
                    b.Navigation("LocationInventory");
                });

            modelBuilder.Entity("ToyStore.Models.DBModels.Order", b =>
                {
                    b.Navigation("cart");
                });
#pragma warning restore 612, 618
        }
    }
}
