using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceShopping
{
    public interface IShopping : IGeneric<Shopping>
    {
        public Task<Shopping> PurchaseByState(string userId, EnumBuyState state);
    }
}

