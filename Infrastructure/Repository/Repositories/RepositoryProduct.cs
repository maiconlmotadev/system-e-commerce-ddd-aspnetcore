using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryProduct : RepositoryGenerics<Product>, IProduct
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public RepositoryProduct()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Product>> ListProducts(Expression<Func<Product, bool>> exProduct)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await banco.Product.Where(exProduct).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Product>> ListProductsUserCart(string userId)
        {
            using (var bank = new ContextBase(_optionsBuilder))
            {
                var productsUserCart = await (from p in bank.Product
                                              join c in bank.UserBuy on p.Id equals c.IdProduct
                                              where c.UserId.Equals(userId) && c.State == EnumBuyState.Product_Cart
                                              select new Product
                                              {
                                                Id = p.Id,
                                                Name = p.Name,
                                                Description = p.Description,
                                                Observation = p.Observation,
                                                Price = p.Price,
                                                BuyQuant = c.BuyQuantity, 
                                                IdCartProduct = c.Id,
                                                Url = p.Url,

                                              }).AsNoTracking().ToListAsync();
                return productsUserCart;
            }
        }

        public async Task<Product> GetProductsUserCart(int idCartProduct)
        {
            using (var bank = new ContextBase(_optionsBuilder))
            {
                var productsUserCart = await (from p in bank.Product
                                              join c in bank.UserBuy on p.Id equals c.IdProduct
                                              where c.Id.Equals(idCartProduct) && c.State == EnumBuyState.Product_Cart
                                              select new Product
                                              {
                                                  Id = p.Id,
                                                  Name = p.Name,
                                                  Description = p.Description,
                                                  Observation = p.Observation,
                                                  Price = p.Price,
                                                  BuyQuant = c.BuyQuantity,
                                                  IdCartProduct = c.Id,
                                                  Url = p.Url,
                                              }).AsNoTracking().FirstOrDefaultAsync();

                return productsUserCart;
            }
        }


        public async Task<List<Product>> ListProductsUser(string userId)
        {
            using (var bank = new ContextBase(_optionsBuilder))
            {
                return await bank.Product.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }
        }
    }
}
