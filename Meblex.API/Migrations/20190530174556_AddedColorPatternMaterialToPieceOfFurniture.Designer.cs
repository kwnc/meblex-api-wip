﻿// <auto-generated />
using System;
using Meblex.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Meblex.API.Migrations
{
    [DbContext(typeof(MeblexDbContext))]
    [Migration("20190530174556_AddedColorPatternMaterialToPieceOfFurniture")]
    partial class AddedColorPatternMaterialToPieceOfFurniture
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Meblex.API.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(132);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Meblex.API.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NIP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("PostCode");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("UserId");

                    b.HasKey("ClientId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Meblex.API.Models.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HexCode")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("ColorId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Meblex.API.Models.CustomSizeForm", b =>
                {
                    b.Property<int>("CustomSizeFormId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<int>("ClientId");

                    b.Property<int>("PieceOfFurnitureId");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("CustomSizeFormId");

                    b.HasIndex("ClientId");

                    b.HasIndex("PieceOfFurnitureId");

                    b.ToTable("CustomSizeForms");
                });

            modelBuilder.Entity("Meblex.API.Models.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("MaterialId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Meblex.API.Models.MaterialPhoto", b =>
                {
                    b.Property<int>("MaterialPhotoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaterialId");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(132);

                    b.HasKey("MaterialPhotoId");

                    b.HasIndex("MaterialId")
                        .IsUnique();

                    b.ToTable("MaterialPhotos");
                });

            modelBuilder.Entity("Meblex.API.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("ClientId");

                    b.Property<string>("Delivery")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("PostCode");

                    b.Property<bool>("Reservation");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("TransactionId");

                    b.HasKey("OrderId");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Meblex.API.Models.OrderLine", b =>
                {
                    b.Property<int>("OrderLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int>("OrderId");

                    b.Property<int?>("PartId");

                    b.Property<int?>("PieceOfFurnitureId");

                    b.Property<int>("Price");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("OrderLineId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PartId")
                        .IsUnique();

                    b.HasIndex("PieceOfFurnitureId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("Meblex.API.Models.Part", b =>
                {
                    b.Property<int>("PartId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColorId");

                    b.Property<int>("Count");

                    b.Property<int>("MaterialId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("PatternId");

                    b.Property<int>("PieceOfFurnitureId");

                    b.Property<float>("Price");

                    b.HasKey("PartId");

                    b.HasIndex("ColorId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("PatternId");

                    b.HasIndex("PieceOfFurnitureId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("Meblex.API.Models.Pattern", b =>
                {
                    b.Property<int>("PatternId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("PatternId");

                    b.ToTable("Patterns");
                });

            modelBuilder.Entity("Meblex.API.Models.PatternPhoto", b =>
                {
                    b.Property<int>("PatternPhotoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(132);

                    b.Property<int>("PatternId");

                    b.HasKey("PatternPhotoId");

                    b.HasIndex("PatternId")
                        .IsUnique();

                    b.ToTable("PatternPhotos");
                });

            modelBuilder.Entity("Meblex.API.Models.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(132);

                    b.Property<int>("PieceOfFurnitureId");

                    b.HasKey("PhotoId");

                    b.HasIndex("PieceOfFurnitureId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Meblex.API.Models.PieceOfFurniture", b =>
                {
                    b.Property<int>("PieceOfFurnitureId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("ColorId");

                    b.Property<int>("Count");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("MaterialId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("PatternId");

                    b.Property<double>("Price");

                    b.Property<int>("RoomId");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("PieceOfFurnitureId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ColorId")
                        .IsUnique();

                    b.HasIndex("MaterialId")
                        .IsUnique();

                    b.HasIndex("PatternId")
                        .IsUnique();

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("Furniture");
                });

            modelBuilder.Entity("Meblex.API.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Meblex.API.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Meblex.API.Models.Client", b =>
                {
                    b.HasOne("Meblex.API.Models.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("Meblex.API.Models.Client", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meblex.API.Models.CustomSizeForm", b =>
                {
                    b.HasOne("Meblex.API.Models.Client", "Client")
                        .WithMany("CustomSizeForms")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.PieceOfFurniture", "PieceOfFurniture")
                        .WithMany("CustomSizeForms")
                        .HasForeignKey("PieceOfFurnitureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meblex.API.Models.MaterialPhoto", b =>
                {
                    b.HasOne("Meblex.API.Models.Material", "Material")
                        .WithOne("Photo")
                        .HasForeignKey("Meblex.API.Models.MaterialPhoto", "MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meblex.API.Models.Order", b =>
                {
                    b.HasOne("Meblex.API.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meblex.API.Models.OrderLine", b =>
                {
                    b.HasOne("Meblex.API.Models.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.Part", "Part")
                        .WithOne("OrderLine")
                        .HasForeignKey("Meblex.API.Models.OrderLine", "PartId");

                    b.HasOne("Meblex.API.Models.PieceOfFurniture", "PieceOfFurniture")
                        .WithMany("OrderLines")
                        .HasForeignKey("PieceOfFurnitureId");
                });

            modelBuilder.Entity("Meblex.API.Models.Part", b =>
                {
                    b.HasOne("Meblex.API.Models.Color", "Color")
                        .WithMany("Parts")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.Material", "Material")
                        .WithMany("Parts")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.Pattern", "Pattern")
                        .WithMany("Parts")
                        .HasForeignKey("PatternId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.PieceOfFurniture", "PieceOfFurniture")
                        .WithMany("Parts")
                        .HasForeignKey("PieceOfFurnitureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meblex.API.Models.PatternPhoto", b =>
                {
                    b.HasOne("Meblex.API.Models.Pattern", "Pattern")
                        .WithOne("Photo")
                        .HasForeignKey("Meblex.API.Models.PatternPhoto", "PatternId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meblex.API.Models.Photo", b =>
                {
                    b.HasOne("Meblex.API.Models.PieceOfFurniture", "PieceOfFurniture")
                        .WithMany("Photos")
                        .HasForeignKey("PieceOfFurnitureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Meblex.API.Models.PieceOfFurniture", b =>
                {
                    b.HasOne("Meblex.API.Models.Category", "Category")
                        .WithMany("Furniture")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.Color", "Color")
                        .WithOne("PieceOfFurniture")
                        .HasForeignKey("Meblex.API.Models.PieceOfFurniture", "ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.Material", "Material")
                        .WithOne("PieceOfFurniture")
                        .HasForeignKey("Meblex.API.Models.PieceOfFurniture", "MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.Pattern", "Pattern")
                        .WithOne("PieceOfFurniture")
                        .HasForeignKey("Meblex.API.Models.PieceOfFurniture", "PatternId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meblex.API.Models.Room", "Room")
                        .WithOne("PieceOfFurniture")
                        .HasForeignKey("Meblex.API.Models.PieceOfFurniture", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
