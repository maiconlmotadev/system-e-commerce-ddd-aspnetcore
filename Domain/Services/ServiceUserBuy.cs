using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.InterfaceShopping;
using Domain.Interfaces.UserBuyInterface;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceUserBuy : IServiceUserBuy
    {
        private readonly IUserBuy _IUserBuy;
        private readonly IShopping _IShopping;
        public ServiceUserBuy(IUserBuy IUserBuy, IShopping IShopping) 
        {
            _IUserBuy = IUserBuy;
            _IShopping = IShopping;
        }

        public async Task AddProduct(string userId, UserBuy userBuy)
        {
            var purchase = await _IShopping.PurchaseByState(userId, EnumBuyState.Product_Cart);
            if (purchase == null)
            {
                purchase = new Shopping
                {
                    UserId = userId,
                    State = EnumBuyState.Product_Cart
                };
                await _IShopping.Add(purchase);
            }

            if (purchase.Id > 0)
            {
                userBuy.ShoppingId = purchase.Id;
                await _IUserBuy.Add(userBuy);
            };
        }

        public async Task<UserBuy> BuyCart(string userId)
        {
            return await _IUserBuy.ProductsPurchasedByState(userId, EnumBuyState.Product_Cart);
        }


        public async Task<List<UserBuy>> MyPurchases(string userId)
        {
            return await _IUserBuy.MyPurchasedByState(userId, EnumBuyState.Product_Purchased);
        }

        public async Task<UserBuy> BuyProducts(string userId, int? purchaseId = null)
        {
            return await _IUserBuy.ProductsPurchasedByState(userId, EnumBuyState.Product_Purchased, purchaseId);
        }

        
    }
}
     