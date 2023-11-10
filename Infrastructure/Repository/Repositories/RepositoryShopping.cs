using Domain.Interfaces.InterfaceShopping;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    
    public class RepositoryShopping : RepositoryGenerics<Shopping>, IShopping
    {

        private readonly DbContextOptions<ContextBase> _optionsbuilder;

        public RepositoryShopping()
        {
            _optionsbuilder = new DbContextOptions<ContextBase>();
        }


        public async Task<Shopping> PurchaseByState(string userId, EnumBuyState state)
        {
            using (var db = new ContextBase(_optionsbuilder))
            {
                return await db.Shopping.FirstOrDefaultAsync(s => s.State == state && s.UserId == userId);
            }
        }
    }
}
