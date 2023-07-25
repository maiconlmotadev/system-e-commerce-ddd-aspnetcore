using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var validatePrice = product.ValidateDecimalProperty(product.Price, "Price");
            var validateStockQuantity = product.ValidateIntProperty(product.StockQuantity, "StockQuantity");

            if (validateName && validatePrice && validateStockQuantity)
            {
                product.RegistrationDate= DateTime.Now;
                product.ChangeDate= DateTime.Now;
                product.State = true;
                await _IProduct.Add(product);
            }
        }

        public async Task<List<Product>> ListProductsWithStock(Expression<Func<Product, bool>> exPression)
        {
            return await _IProduct.ListProducts(p => p.StockQuantity > 0);
        }

        public async Task UpdateProduct(Product product)
        {
            var validateName = product.ValidateStringProperty(product.Name, "Name");
            var validatePrice = product.ValidateDecimalProperty(product.Price, "Price");
            var validateStockQuantity = product.ValidateIntProperty(product.StockQuantity, "StockQuantity");

            if (validateName && validatePrice && validateStockQuantity)
            {
                product.ChangeDate = DateTime.Now;
                await _IProduct.Update(product);
            }
        }
    }
}
