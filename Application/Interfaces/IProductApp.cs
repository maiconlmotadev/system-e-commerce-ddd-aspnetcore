﻿using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface IProductApp : IGenericApp<Product>
    {
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);

    }
}
