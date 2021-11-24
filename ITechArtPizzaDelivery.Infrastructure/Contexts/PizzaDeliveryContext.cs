using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Contexts
{
    public class PizzaDeliveryContext :DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Promocode> Promocodes { get; set; }

        public PizzaDeliveryContext(DbContextOptions<PizzaDeliveryContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Pizzas)
                .WithMany(p => p.Carts)
                .UsingEntity<CartPizza>(
                    j => j
                        .HasOne(cp => cp.Pizza)
                        .WithMany(c => c.CartPizzas)
                        .HasForeignKey(cp => cp.PizzaId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j=>j
                        .HasOne(cp => cp.Cart)
                        .WithMany(c => c.CartPizzas)
                        .HasForeignKey(cp => cp.CartId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey(cp => new {cp.CartId, cp.PizzaId});
                        j.ToTable("carts_pizzas");
                    }
                );

            modelBuilder.Entity<User>(u =>
            {
                u
                    .HasOne(u => u.Cart)
                    .WithMany()
                    .HasForeignKey("CartId")
                    .OnDelete(DeleteBehavior.SetNull);

                u.Navigation(u => u.Cart);
            });

            modelBuilder.Entity<Promocode>()
                .HasIndex(p => p.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    FirstName = "test",
                    LastName = "user",
                    Role = Role.User
                });
        }
    }
}
