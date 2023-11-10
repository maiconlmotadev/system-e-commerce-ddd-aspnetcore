using Domain.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceProduct
{
    public interface IProduct : IGeneric<Product>
    { 
        Task<List<Product>> ListProductsUser(string userId);

        Task<List<Product>> ListProducts(Expression<Func<Product, bool>> exProduct);

        Task<List<Product>> ListProductsUserCart(string userId);

        Task<Product> GetProductsUserCart(int idCartProduct);
    }
}
