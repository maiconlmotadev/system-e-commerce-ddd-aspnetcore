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
    }
}
     