using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Pagination;
using Microsoft.AspNetCore.Http;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPizzasService
    {
        Task<List<Pizza>> GetAll();
        Task<PagedList<Pizza>> GetAll(PagingParameters parameters);
        Task<Pizza> GetById(int id);
        Task<Pizza> Post(Pizza pizza);
        public Task<Pizza> AddIngredientsToPizza(int pizzaId, int[] ingredientsId);
        public Task AddImageToPizza(int pizzaId, IFormFile image);
        public Task<FileStream> DownloadImage(int pizzaId);
        public Task DeleteById(int id);
    }
}