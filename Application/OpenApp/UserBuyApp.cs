using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.UserBuyInterface;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class UserBuyApp : IUserBuyApp
    {

        private readonly IUserBuy _IUserBuy;
        private readonly IServiceUserBuy _IServiceUserBuy;

        public UserBuyApp(IUserBuy IUserBuy, IServiceUserBuy IServiceUserBuy)
        {
            _IUserBuy = IUserBuy;
            _IServiceUserBuy = IServiceUserBuy;

        }

        public async Task<UserBuy> BuyCart(string userId)
        {
            return await _IServiceUserBuy.BuyCart(userId);
        }

        public async Task<UserBuy> BuyProducts(string userId, int? purchaseId = null)
        {
            return await _IServiceUserBuy.BuyProducts(userId, purchaseId);
        }

        public async Task<bool> ConfirmPurchaseCartUser(string userId)
        {
            return await _IUserBuy.ConfirmPurchaseUserCart(userId);
        }



        public async Task<int> QuantProductUserCart(string userId)
        {
            return await _IUserBuy.QuantProductUserCart(userId);
        }

        public async Task Add(UserBuy Object)
        {
            await _IUserBuy.Add(Object);
        }

        public async Task Delete(UserBuy Object)
        {
            await _IUserBuy.Delete(Object);
        }

        public async Task<UserBuy> GetEntityById(int Id)
        {
            return await _IUserBuy.GetEntityById(Id);
        }

        public async Task<List<UserBuy>> List()
        {
            return await _IUserBuy.List();
        }



        public async Task Update(UserBuy Object)
        {
            await _IUserBuy.Update(Object);
        }

        public async Task<List<UserBuy>> MyPurchases(string userId)
        {
            return await _IServiceUserBuy.MyPurchases(userId);
        }

        public async Task AddProduct(string userId, UserBuy userBuy)
        {
            await _IServiceUserBuy.AddProduct(userId, userBuy);
        }
    }
}
