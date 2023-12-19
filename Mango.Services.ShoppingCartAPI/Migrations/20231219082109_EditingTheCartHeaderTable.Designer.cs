﻿// <auto-generated />
using Mango.Services.ShoppingCartAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.ShoppingCartAPI.Migrations
{
    [DbContext(typeof(ShoppinCartDB_Context))]
    [Migration("20231219082109_EditingTheCartHeaderTable")]
    partial class EditingTheCartHeaderTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mango.Services.ShoppingCartAPI.Model.CartDetails", b =>
                {
                    b.Property<int>("CartDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartDetailsID"));

                    b.Property<int>("CartHeaderID")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("CartDetailsID");

                    b.HasIndex("CartHeaderID");

                    b.ToTable("CartDetails");
                });

            modelBuilder.Entity("Mango.Services.ShoppingCartAPI.Model.CartHeader", b =>
                {
                    b.Property<int>("CartHeaderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartHeaderID"));

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CartHeaderID");

                    b.ToTable("CartHeaders");
                });

            modelBuilder.Entity("Mango.Services.ShoppingCartAPI.Model.CartDetails", b =>
                {
                    b.HasOne("Mango.Services.ShoppingCartAPI.Model.CartHeader", "CartHeader")
                        .WithMany()
                        .HasForeignKey("CartHeaderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartHeader");
                });
#pragma warning restore 612, 618
        }
    }
}
