using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class ProductApp : IProductApp
    {
        IProduct _IProduct;
        IServiceProduct _IServiceProduct;

        public ProductApp(IProduct IProduct, IServiceProduct IServiceProduct)
        {
            _IProduct = IProduct;
            _IServiceProduct = IServiceProduct;
        }

        public async Task<List<Product>> ListProductsUserCart(string userId)
        {
            return await _IProduct.ListProductsUserCart(userId);
        }

        public async Task<Product> GetProductsCart(int idCartProduct)
        {
            return await _IProduct.GetProductsUserCart(idCartProduct);
        }

        public async Task AddProduct(Product product)
        {
            await _IServiceProduct.AddProduct(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _IServiceProduct.UpdateProduct(product);
        }

        public async Task<List<Product>> ListUserPoduct(string userId)
        {
            return await _IProduct.ListProductsUser(userId);
        }

        public async Task Add(Product Object)
        {
            await _IProduct.Add(Object);
        }

        public async Task Delete(Product Object)
        {
            await _IProduct.Delete(Object);
        }

        public async Task<Product> GetEntityById(int Id)
        {
           return await _IProduct.GetEntityById(Id);
        }

        public async Task<List<Product>> List()
        {
            return await _IProduct.List();
        }

        public async Task Update(Product Object)
        {
            await _IProduct.Update(Object);
        }

        public async Task<List<Product>> ListProductsWithStock(string description)
        {
            return await _IServiceProduct.ListProductsWithStock(description);
        }

 
    }
}