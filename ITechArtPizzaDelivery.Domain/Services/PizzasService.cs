using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Errors;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public class PizzasService : IPizzasService
    {
        private readonly IPizzasRepository _pizzasRepository;
        private readonly IGenericRepository<Ingredient> _ingredientsRepository;

        public PizzasService(IPizzasRepository pizzasRepository, IGenericRepository<Ingredient> ingredientsRepository)
        {
            _pizzasRepository = pizzasRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<List<Pizza>> GetAll()
        {
            return await _pizzasRepository.GetAll();
        }

        public async Task<PagedList<Pizza>> GetAll(PagingParameters parameters)
        {
            return await _pizzasRepository.GetAll(parameters);
        }

        public async Task<Pizza> GetById(int id)
        {
            return await _pizzasRepository.GetPizzaWithIngredients(id);
        }
        public async Task<Pizza> Post(Pizza pizza)
        {
            return await _pizzasRepository.Insert(pizza);
        }

        public async Task<Pizza> AddIngredientsToPizza(int pizzaId, int[] ingredientsId)
        {
            var pizza = await _pizzasRepository.GetPizzaWithIngredients(pizzaId);
            var pizzaIngredientsId = pizza.Ingredients.Select(i => i.Id).ToList();
            
            var ingredients = await _ingredientsRepository.GetAll();
            var newIngredients = ingredients    
                .Where(i => ingredientsId.Contains(i.Id) && !pizzaIngredientsId.Contains(i.Id))
                .ToList();
            if (newIngredients.Count == 0)
            {
                throw new KeyNotFoundException("New ingredients not found");
            }

            pizza.Ingredients.AddRange(ingredients);
            await _pizzasRepository.Update(pizza);
            return pizza;
        }

        public async Task AddImageToPizza(int pizzaId, IFormFile image)
        {
            var pizza = await _pizzasRepository.GetById(pizzaId);

            if (pizza.Image is not null)
            {
                throw new BadRequestException("Pizza already has an image");
            }
            
            var path = "./Images/" + Guid.NewGuid();
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            pizza.Image = path;
            await _pizzasRepository.Update(pizza);
        }

        public async Task<FileStream> DownloadImage(int pizzaId)
        {
            var pizza = await _pizzasRepository.GetById(pizzaId);

            if (pizza.Image is null)
            {
                throw new BadRequestException("Pizza has not image");
            }
            
            var fileStream = new FileStream(pizza.Image, FileMode.Open);
            return fileStream;
        }
        
        public async Task DeleteById(int id)
        {
            await _pizzasRepository.Delete(id);
        }
    }
}
