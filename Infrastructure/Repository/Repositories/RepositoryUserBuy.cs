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
            using (var db = new ContextBase(_optionsbuilder))
            {
                var userBuy = new UserBuy();
                userBuy.ProductsList = new List<Product>();

                var productsUserCart = await (from p in db.Product
                                              join s in db.UserBuy on p.Id equals s.IdProduct
                                              join sh in db.Shopping on s.ShoppingId equals sh.Id
                                              where s.UserId.Equals(userId) && s.State == state &&
                                              sh.UserId.Equals(userId) && sh.State == state
                                              select new Product
                                              {
                                                  Id = p.Id,
                                                  Name = p.Name,
                                                  Description = p.Description,
                                                  Observation = p.Observation,
                                                  Price = p.Price,
                                                  BuyQuant = s.BuyQuantity,
                                                  IdCartProduct = s.Id,
                                                  Url = p.Url,
                                                  PurchaseDate = sh.PurchaseDate, 

                                              }).AsNoTracking().ToListAsync();

                
                userBuy.ProductsList = productsUserCart;             
                userBuy.ApplicationUser = await db.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));                          
                userBuy.ProductsQuantity = productsUserCart.Count();
                userBuy.DeliveryAddress = string.Concat(userBuy.ApplicationUser.Address, " - ", userBuy.ApplicationUser.AddressComplement, " - CodPost: ", userBuy.ApplicationUser.CPost);
                userBuy.TotalPrice = productsUserCart.Sum(v => v.Price);
                userBuy.State = state;
                return userBuy;


            }
        }

        public async Task<int> QuantProductUserCart(string userId)
        {
            using (var db = new ContextBase(_optionsbuilder))
            {
                return await db.UserBuy.CountAsync(s => s.UserId.Equals(userId) && s.State == EnumBuyState.Product_Cart);
            }
        }

        public async Task<List<UserBuy>> MyPurchasedByState(string userId, EnumBuyState state)
        {
            var result = new List<UserBuy>();

            using (var db = new ContextBase(_optionsbuilder))
            {
                var userPurchases = await db.Shopping
                    .Where(sh => sh.State == state && sh.UserId.Equals(userId)).ToListAsync();

                foreach (var item in userPurchases)
                {
                    var cuserPurchase = new UserBuy();
                    cuserPurchase.ProductsList = new List<Product>();

                    var userCartProducts = await (from p in db.Product
                                                         join s in db.UserBuy on p.Id equals s.IdProduct
                                                         where s.UserId.Equals(userId) && s.State == state && s.ShoppingId == item.Id
                                                         select new Product
                                                         {
                                                             Id = p.Id,
                                                             Name = p.Name,
                                                             Description = p.Description,
                                                             Observation = p.Observation,
                                                             Price = p.Price,
                                                             BuyQuant = s.BuyQuantity,
                                                             IdCartProduct = s.Id,
                                                             Url = p.Url,
                                                             PurchaseDate = item.PurchaseDate,

                                                         }).AsNoTracking().ToListAsync();

                    cuserPurchase.ProductsList = userCartProducts;
                    cuserPurchase.ApplicationUser = await db.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                    cuserPurchase.ProductsQuantity = userCartProducts.Count();
                    cuserPurchase.DeliveryAddress = string.Concat(cuserPurchase.ApplicationUser.Address, " - ", cuserPurchase.ApplicationUser.AddressComplement, " - Postal code: ", cuserPurchase.ApplicationUser.CPost);
                    cuserPurchase.TotalPrice = userCartProducts.Sum(v => v.Price);
                    cuserPurchase.State = state;
                    cuserPurchase.Id = item.Id;

                    result.Add(cuserPurchase);
                }

                return result;

            }
        }
    }
}
