using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPizzasRepository
    {
        public List<Pizza> GetAll();
        public Pizza GetById(int id);
    }
}
