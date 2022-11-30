using Microsoft.EntityFrameworkCore;
using NetChallenge.Database.Interfaces;
using NetChallenge.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChallenge.Database
{
    public class ApiRepository : IApiRepository
    {
        private readonly DotNetChallengeContext _context;
        public ApiRepository(DotNetChallengeContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductByDescriptionAsync(string query)
        {
            var products = await _context.Products.Where(x => x.Description.Contains(query)).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
