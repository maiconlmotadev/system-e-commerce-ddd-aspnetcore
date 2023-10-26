using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.UserBuyInterface;
using Entities.Entities;
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
        public ServiceUserBuy(IUserBuy IUserBuy) 
        {
            _IUserBuy = IUserBuy;
        }

        public async Task<UserBuy> BuyCart(string userId)
        {
            return await _IUserBuy.ProductsPurchasedByState(userId, Entities.Entities.Enums.EnumBuyState.Product_Cart);
        }

        public async Task<List<UserBuy>> MyPurchases(string userId)
        {
            return await _IUserBuy.MyPurchasedByState(userId, Entities.Entities.Enums.EnumBuyState.Product_Purchased);
        }

        public async Task<UserBuy> BuyProducts(string userId, int? purchaseId = null)
        {
            return await _IUserBuy.ProductsPurchasedByState(userId, Entities.Entities.Enums.EnumBuyState.Product_Purchased, purchaseId);
        }
    }
}
     