using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        public Task<List<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<T> Insert(T obj);
        public Task Update(T obj);
        public Task Delete(int id);
    }
}