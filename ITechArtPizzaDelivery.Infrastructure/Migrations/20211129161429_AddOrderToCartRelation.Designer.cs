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
    [Migration("20211129161429_AddOrderToCartRelation")]
    partial class AddOrderToCartRelation
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.CartPizza", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.HasKey("CartId", "PizzaId");

                    b.HasIndex("PizzaId");

                    b.ToTable("carts_pizzas");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("DeliveryId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int?>("PromocodeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.HasIndex("PromocodeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Promocodes");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "test",
                            LastName = "user",
                            Role = 1
                        });
                });

            modelBuilder.Entity("IngredientPizza", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.Property<int>("PizzasId")
                        .HasColumnType("int");

                    b.HasKey("IngredientsId", "PizzasId");

                    b.HasIndex("PizzasId");

                    b.ToTable("IngredientPizza");
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.Order", b =>
                {
                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Cart", "Cart")
                        .WithOne("Order")
                        .HasForeignKey("ITechArtPizzaDelivery.Domain.Models.Order", "CartId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.User", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Delivery", "Delivery")
                        .WithMany("Order")
                        .HasForeignKey("DeliveryId")
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

                    b.Navigation("Cart");

                    b.Navigation("Customer");

                    b.Navigation("Delivery");

                    b.Navigation("Payment");

                    b.Navigation("Promocode");
                });

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.User", b =>
                {
                    b.HasOne("ITechArtPizzaDelivery.Domain.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Cart");
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

                    b.Navigation("Order");
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

            modelBuilder.Entity("ITechArtPizzaDelivery.Domain.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}