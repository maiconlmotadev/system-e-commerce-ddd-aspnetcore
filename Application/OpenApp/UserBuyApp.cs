using ApplicationApp.Interfaces;
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
        public UserBuyApp(IUserBuy IUserBuy)
        {
            _IUserBuy = IUserBuy;
        }

        public async Task<int> QuantProductUserCart(string userId)
        {
            return await _IUserBuy.quantProductUserCart(userId);
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
    }
}
