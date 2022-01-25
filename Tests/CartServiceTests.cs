using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Services;
using Moq;
using Xunit;

namespace Tests
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _repository;

        public CartServiceTests()
        {
            _repository = new Mock<ICartRepository>();
        }

        [Fact]
        public void GetPizzasFromCart_ListOfPizzas_UserExists()
        {
            // arrange 
            var pizzas = GetSampleCart();
            var customerId = 1;
            _repository.Setup(x => x.GetPizzasFromCart(customerId))
                .ReturnsAsync(pizzas);
            var service = new CartService(_repository.Object);
            
            // act
            var result = service.GetPizzasFromCart(customerId).Result;
            
            // assert
            Assert.Equal(pizzas, result);
        }
        
        [Fact]
        public void AddPizzaToCart_ListOfPizzas_UserExistsPizzaExists()
        {
            // arrange 
            var pizzas = GetSampleCart();
            var customerId = 1;
            var pizzaId = 1;
            var pizzasCount = 2;
            _repository.Setup(x => x.AddPizzaToCart(pizzaId, customerId, pizzasCount))
                .ReturnsAsync(pizzas);
            var service = new CartService(_repository.Object);
            
            // act
            var result = service.AddPizzaToCart(pizzaId,customerId,pizzasCount).Result;
            
            // assert
            Assert.Equal(pizzas, result);
        }
        
        [Fact]
        public void AddPizzaToCart_KeyNotFoundException_UserOrPizzaNotExist()
        {
            // arrange 
            var pizzas = GetSampleCart();
            var customerId = 1;
            var pizzaId = 1;
            var pizzasCount = 2;
            _repository.Setup(x => x.AddPizzaToCart(pizzaId, customerId, pizzasCount))
                .ThrowsAsync(new KeyNotFoundException());
            var service = new CartService(_repository.Object);
            
            // act & assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => service.AddPizzaToCart(pizzaId,customerId,pizzasCount)).Wait();
        }
        
        [Fact]
        public void GetPizzasFromCart_KeyNotFoundException_UserNotExists()
        {
            // arrange 
            var pizzas = GetSampleCart();
            var customerId = 1;
            _repository.Setup(x => x.GetPizzasFromCart(customerId))
                .ThrowsAsync(new KeyNotFoundException());
            var service = new CartService(_repository.Object);
            
            // act & assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetPizzasFromCart(customerId)).Wait();
        }
        
        [Fact]
        public void DeleteByPizzaId_PizzaRemoved_UserExistsPizzaExists()
        {
            // arrange 
            var pizzas = GetSampleCart();
            var customerId = 1;
            var pizzaId = 1;
            var removablePizza = pizzas[pizzaId - 1];
            _repository.Setup(x => x.DeleteByPizzaId(customerId, pizzaId))
                .Callback((int customerId, int pizzaId) => { pizzas.RemoveAt(pizzaId - 1); });
            var service = new CartService(_repository.Object);
            
            // act
            service.DeleteByPizzaId(customerId,pizzaId).Wait();
            
            // assert
            Assert.DoesNotContain(removablePizza, pizzas);
        }
        
        [Fact]
        public void DeleteByPizzaId_KeyNotFoundException_UserOrPizzaNotExist()
        {
            // arrange 
            var pizzas = GetSampleCart();
            var customerId = 1;
            var pizzaId = 1;
            var removablePizza = pizzas[pizzaId - 1];
            _repository.Setup(x => x.DeleteByPizzaId(customerId, pizzaId))
                .ThrowsAsync(new KeyNotFoundException());
            var service = new CartService(_repository.Object);
            
            // act & assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => service.DeleteByPizzaId(customerId,pizzaId)).Wait();
        }
        
        [Fact]
        public void ChangeAmountOfPizza_AmountChanged_PizzaInTheCart()
        {
            // arrange
            var pizzas = GetSampleCart();
            var customerId = 1;
            _repository.Setup(x => x.GetPizzasFromCart(customerId))
                .ReturnsAsync(pizzas);
            var service = new CartService(_repository.Object);
            
            var pizzaId = 2;
            var count = 2;
            
            // act
            var task = service.ChangeAmountOfPizza(pizzaId,customerId, count);
            
            // assert
            task.Wait();
            Assert.Equal(count,pizzas[pizzaId-1].Count);
        }
        
        [Fact]
        public void ChangeAmountOfPizza_KeyNotFoundException_PizzaNotInTheCart()
        {
            // arrange
            var pizzas = GetSampleCart();
            var customerId = 1;
            _repository.Setup(x => x.GetPizzasFromCart(customerId))
                .ReturnsAsync(pizzas);
            var service = new CartService(_repository.Object);
            
            var pizzaId = 4;
            var count = 2;
            
            // act & assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => service.ChangeAmountOfPizza(pizzaId,customerId, count))
                .Wait();
        }
        
        [Fact]
        public void ChangeAmountOfPizza_KeyNotFoundException_UserNotExists()
        {
            // arrange
            var pizzas = GetSampleCart();
            var customerId = 1;
            _repository.Setup(x => x.GetPizzasFromCart(customerId))
                .ThrowsAsync(new KeyNotFoundException());
            var service = new CartService(_repository.Object);
            
            var pizzaId = 4;
            var count = 2;
            
            // act & assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => service.ChangeAmountOfPizza(pizzaId,customerId, count))
                .Wait();
        }


        private List<CartPizza> GetSampleCart()
        {
            List<CartPizza> output = new()
            {
                new CartPizza
                {
                    Count = 1,
                    PizzaId = 1
                },
                new CartPizza
                {
                    Count = 10,
                    PizzaId = 2
                },
                new CartPizza
                {
                    Count = 7,
                    PizzaId = 3
                },
            };
            return output;
        }
        
        private List<Pizza> GetSamplePizzas()
        {
            List<Pizza> output = new()
            {
                new Pizza
                {
                    Id = 1,
                    Image = "string",
                    Name = "name",
                    Price = 10
                },
                new Pizza
                {
                    Id = 2,
                    Image = "string2",
                    Name = "name2",
                    Price = 150
                },
                new Pizza
                {
                    Id = 3,
                    Image = "string3",
                    Name = "name3",
                    Price = 15
                }
            };
            return output;
        }
    }
}