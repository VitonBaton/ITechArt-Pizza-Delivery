﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IIngredientsRepository
    {
        public Task<List<Ingredient>> GetAll();
        public Task<Ingredient> GetById(long id);
        public Task<Ingredient> Post(Ingredient ingredient);
        public Task DeleteById(long id);
    }
}
