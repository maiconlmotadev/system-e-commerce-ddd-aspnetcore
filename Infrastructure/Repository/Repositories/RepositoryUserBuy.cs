using Domain.Interfaces.UserBuyInterface;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryUserBuy : RepositoryGenerics<UserBuy>, IUserBuy
    {
        private readonly DbContextOptions<ContextBase> _optionsbuilder;

        public RepositoryUserBuy()
        {
            _optionsbuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<bool> ConfirmPurchaseUserCart(string userId)
        {

            try
            {

            using (var bank = new ContextBase(_optionsbuilder))
            {
                var userBuy = new UserBuy();
                userBuy.ProductsList = new List<Product>();

                var productsUserCart = await (from p in bank.Product
                                              join c in bank.UserBuy on p.Id equals c.IdProduct
                                              where c.UserId.Equals(userId) && c.State == EnumBuyState.Product_Cart
                                              select c).AsNoTracking().ToListAsync();

                productsUserCart.ForEach(p =>
                {
                    p.State = EnumBuyState.Product_Purchased;
                });

                bank.UpdateRange(productsUserCart);
                await bank.SaveChangesAsync();

                return true;                                 
            }
            }
            catch (Exception error)
            {
                return false;
            }

        }

        public async Task<UserBuy> ProductsPurchasedByState(string userId, EnumBuyState state)
        {
            using (var bank = new ContextBase(_optionsbuilder))
            {
                var userBuy = new UserBuy();
                userBuy.ProductsList = new List<Product>();

                var productsUserCart = await (from p in bank.Product
                                              join c in bank.UserBuy on p.Id equals c.IdProduct
                                              where c.UserId.Equals(userId) && c.State == state
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

                
                userBuy.ProductsList = productsUserCart;             
                userBuy.ApplicationUser = await bank.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));                          
                userBuy.ProductsQuantity = productsUserCart.Count();
                userBuy.DeliveryAddress = string.Concat(userBuy.ApplicationUser.Address, " - ", userBuy.ApplicationUser.AddressComplement, " - CodPost: ", userBuy.ApplicationUser.CPost);
                userBuy.TotalPrice = productsUserCart.Sum(v => v.Price);
                userBuy.State = state;
                return userBuy;


            }
        }

        public async Task<int> quantProductUserCart(string userId)
        {
            using (var bank = new ContextBase(_optionsbuilder))
            {
                return await bank.UserBuy.CountAsync(c => c.UserId.Equals(userId) && c.State == EnumBuyState.Product_Cart);
            }
        }
    }
}
