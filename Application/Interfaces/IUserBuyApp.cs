﻿using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface IUserBuyApp : IGenericApp<UserBuy>
    {
        public Task<int> QuantProductUserCart(string userId);

        public Task<UserBuy> BuyCart(string userId);

        public Task<UserBuy> BuyProducts(string userId);

        public Task<bool> ConfirmPurchaseCartUser(string userId);
    }
}
     