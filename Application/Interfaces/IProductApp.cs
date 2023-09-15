using Entities.Entities;
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

        Task<List<Product>> ListUserPoduct(string userId);
        Task<List<Product>> ListProductsWithStock();

        Task<List<Product>> ListProductsUserCart(string userId);

        Task<Product> GetProductsCart(int idCartProduct);
    }
}
