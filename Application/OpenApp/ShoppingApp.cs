using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceShopping;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class ShoppingApp : IShoppingApp
    {

        private readonly IShopping _IShopping;
        public ShoppingApp(IShopping IShopping)
        {
            _IShopping = IShopping;
        }

        public async Task Add(Shopping Object)
        {
            await _IShopping.Add(Object);
        }

        public async Task Delete(Shopping Object)
        {
            await _IShopping.Delete(Object);
        }

        public async Task<Shopping> GetEntityById(int Id)
        {
            return await _IShopping.GetEntityById(Id);
        }

        public async Task<List<Shopping>> List()
        {
            return await _IShopping.List();
        }

        public async Task Update(Shopping Object)
        {
            await _IShopping.Update(Object);
        }
    }
}
