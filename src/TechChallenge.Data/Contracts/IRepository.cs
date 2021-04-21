using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        public Task<T> InsertAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task<bool> DeleteAsync(T entity);
    }
}
