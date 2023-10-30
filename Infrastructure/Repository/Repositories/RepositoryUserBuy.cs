using Domain.Interfaces.UserBuyInterface;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;
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

                using (var db = new ContextBase(_optionsbuilder))
                {
                    var userBuy = new UserBuy();
                    userBuy.ProductsList = new List<Product>();

                    var productsUserCart = await (from p in db.Product
                                                  join s in db.UserBuy on p.Id equals s.IdProduct
                                                  where s.UserId.Equals(userId) && s.State == EnumBuyState.Product_Cart
                                                  select s).AsNoTracking().ToListAsync();

                    productsUserCart.ForEach(p =>
                    {
                        userBuy.ShoppingId = p.ShoppingId;
                        p.State = EnumBuyState.Product_Purchased;
                    });

                    var purchase = await db.Shopping.AsNoTracking().FirstOrDefaultAsync(s => s.Id == userBuy.ShoppingId);
                    if (purchase != null)
                    {
                        purchase.State = EnumBuyState.Product_Purchased;
                    }

                    db.Update(purchase);
                    db.UpdateRange(productsUserCart);
                    await db.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception error)
            {
                return false;
            }

        }

        public async Task<UserBuy> ProductsPurchasedByState(string userId, EnumBuyState state, int? purchaseId = null)
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
                                              && (purchaseId == null || sh.Id == purchaseId)
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
                var userPurchases = await db.Shopping.Where(sh => sh.State == state && sh.UserId.Equals(userId)).ToListAsync();

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
