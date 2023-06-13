using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IProduct _IProduct;
        public ServiceProduct(IProduct IProduct) 
        {
            _IProduct = IProduct;   
        }

        public async Task AddProduct(Product product)
        {
            var validateName = product.ValidateStringProperty(product.Name, "Name");
            var validateValue = product.ValidateDecimalProperty(product.Price, "Price");
            
            if (validateName && validateValue)
            {
                product.State = true;
                await _IProduct.Add(product);
            }
        }
        
        public async Task UpdateProduct(Product product)
        {
            var validateName = product.ValidateStringProperty(product.Name, "Name");
            var validateValue = product.ValidateDecimalProperty(product.Price, "Price");

            if (validateName && validateValue)
            {
                await _IProduct.Update(product);
            }
        }
    }
}
