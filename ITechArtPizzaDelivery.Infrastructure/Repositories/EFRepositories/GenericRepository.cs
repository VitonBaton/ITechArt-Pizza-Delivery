using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Pagination;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        protected readonly PizzaDeliveryContext _dbContext;
        private readonly DbSet<T> _table;

        public GenericRepository(PizzaDeliveryContext context)
        {
            _dbContext = context;
            _table = _dbContext.Set<T>();
        }
        
        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<PagedList<T>> GetAll(PagingParameters parameters)
        {
            var result = await PagedList<T>
                .CreateAsync(_table.AsNoTracking(), parameters.PageNumber, parameters.PageSize);
            return result;
        }

        public async Task<T> GetById(int id)
        {
            var result = await _table.FindAsync(id);
            if (result is null)
            {
                throw new KeyNotFoundException("Item not found");
            }

            return result;
        }

        public async Task<T> Insert(T obj)
        {
            await _table.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task Update(T obj)
        {
            _table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _table.FindAsync(id);
            if (item is null)
            {
                throw new KeyNotFoundException("Item not found");
            }

            _table.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}