using Domain.Interfaces.UserBuyInterface;
using Entities.Entities;
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
    public class RepositoryUserBuy : RepositoryGenerics<UserBuy>, IUserBuy
    {
        private readonly DbContextOptions<ContextBase> _optionsbuilder;

        public RepositoryUserBuy()
        {
            _optionsbuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<int> quantProductUserCart(string userId)
        {
            using (var bank = new ContextBase(_optionsbuilder))
            {
                return await bank.UserBuy.CountAsync(c => c.UserId.Equals(userId));
            }
        }
    }
}
