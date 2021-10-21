using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.Fakes
{
    public class PizzasFakeRepository : IPizzasRepository
    {
        private readonly List<Pizza> pizzas = new()
        {
            new Pizza
            {
                Id = 1,
                Name = "Pepperoni",
                Price = 16.5m,
                Ingredients = new List<string>
                {
                    "Pepperoni", "tomato sauce", "mozzarella"
                }
            },
            new Pizza
            {
                Id = 2,
                Name = "Margaret",
                Price = 14.5m,
                Ingredients = new List<string>
                {
                    "Tomatoes", "tomato sauce", "mozzarella", "oregano"
                }
            }
        };


        public Pizza GetById(int id)
        {
            return pizzas[id - 1];
        }

        List<Pizza> IPizzasRepository.GetAll()
        {
            return pizzas;
        }
    }
}
