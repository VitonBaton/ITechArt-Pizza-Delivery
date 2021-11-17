﻿// <auto-generated />
using System;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ITechArtPizzaDelivery.Infrastructure.Migrations
{
    [DbContext(typeof(PizzaDeliveryContext))]
    [Migration("20211116183319_AddCartsPizzasTable")]
    partial class AddCartsPizzasTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Cart", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.CartPizza", b =>
                {
                    b.Property<long>("CartId")
                        .HasColumnType("bigint");

                    b.Property<long>("PizzaId")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.HasKey("CartId", "PizzaId");

                    b.HasIndex("PizzaId");

                    b.ToTable("carts_pizzas");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Delivery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Ingredient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("DeliveryId")
                        .HasColumnType("bigint");

                    b.Property<long>("PaymentId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5,2)");

                    b.Property<long?>("PromocodeId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId")
                        .IsUnique();

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.HasIndex("PromocodeId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Pizza", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("Pizzas");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Promocode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Promocodes");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IngredientPizza", b =>
                {
                    b.Property<long>("IngredientsId")
                        .HasColumnType("bigint");

                    b.Property<long>("PizzasId")
                        .HasColumnType("bigint");

                    b.HasKey("IngredientsId", "PizzasId");

                    b.HasIndex("PizzasId");

                    b.ToTable("IngredientPizza");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Cart", b =>
                {
                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.CartPizza", b =>
                {
                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Cart", "Cart")
                        .WithMany("CartPizzas")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Pizza", "Pizza")
                        .WithMany("CartPizzas")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Order", b =>
                {
                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Delivery", "Delivery")
                        .WithOne("Order")
                        .HasForeignKey("ITechArtPizzaDelivery.Domain.Models.Order", "DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Payment", "Payment")
                        .WithOne("Order")
                        .HasForeignKey("ITechArtPizzaDelivery.Domain.Models.Order", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Promocode", "Promocode")
                        .WithMany("Orders")
                        .HasForeignKey("PromocodeId");

                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Delivery");

                    b.Navigation("Payment");

                    b.Navigation("Promocode");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("IngredientPizza", b =>
                {
                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Pizza", null)
                        .WithMany()
                        .HasForeignKey("PizzasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Cart", b =>
                {
                    b.Navigation("CartPizzas");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Delivery", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Payment", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Pizza", b =>
                {
                    b.Navigation("CartPizzas");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Promocode", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
