﻿// <auto-generated />
using Mango.Services.ProductsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240523224238_ChangingTheSambossaImageLink")]
    partial class ChangingTheSambossaImageLink
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mango.Services.ProductAPI.Model.Products", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Appetizer",
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://th.bing.com/th/id/OIP.C30KE50Fs-PjhCCljL9q3QHaLH?rs=1&pid=ImgDetMain",
                            Name = "Sambosa",
                            Price = 15.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Appetizer",
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://th.bing.com/th/id/R.2d6497c8af8a41b39d52391c605141dc?rik=PZXUnfSWfAhYUg&pid=ImgRaw&r=0",
                            Name = "Paneer Tikka",
                            Price = 13.99
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Dessert",
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://th.bing.com/th/id/OIP.U2VOKNY3f8Os9ISq6PoG8gHaHU?w=628&h=621&rs=1&pid=ImgDetMain",
                            Name = "Sweet Pie",
                            Price = 10.99
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Entree",
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://th.bing.com/th/id/R.7d1656d63d1f2cd751cd8cb145cbbcc2?rik=L2PKxpb9MrlXzQ&pid=ImgRaw&r=0",
                            Name = "Pav Bhaji",
                            Price = 15.0
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryName = "Main Dish",
                            Description = "Roast Duck has tender and juicy meat, crispy skin, and it's glazed with the honey-balsamic glaze to give the duck a beautiful roasted look.",
                            ImageUrl = "https://s3.amazonaws.com/grazecart/groveladderfarmllc/images/1566559006_5d5fcb1e8100e.jpg",
                            Name = "Roasted Duck",
                            Price = 20.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
