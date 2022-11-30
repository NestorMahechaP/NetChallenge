using NetChallenge.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChallenge.Database.Interfaces
{
    public interface IApiRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetProductByDescriptionAsync(string query);
    }
}
