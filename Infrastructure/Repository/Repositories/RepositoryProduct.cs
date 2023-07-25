using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryProduct : RepositoryGenerics<Product>, IProduct
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        public RepositoryProduct()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Product>> ListProducts(Expression<Func<Product, bool>> exProduct)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await banco.Product.Where(exProduct).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Product>> ListUserProducts(string userId)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await banco.Product.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }
        }
    }
}
