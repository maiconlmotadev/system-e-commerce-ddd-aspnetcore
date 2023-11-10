using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.UserBuyInterface
{
    public interface IUserBuy : IGeneric<UserBuy>
    {
        public Task<int> QuantProductUserCart(string userId);

        public Task<UserBuy> ProductsPurchasedByState(string userId, EnumBuyState state, int? shoppingId = null);

        public Task<List<UserBuy>> MyPurchasedByState(string userId, EnumBuyState state);

        public Task<bool> ConfirmPurchaseUserCart(string userId);

    }
}
