using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EbayAlike.Data.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task Add(T item);

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, string[] children = null);

        public Task Update(T item);

        public Task Remove(T item);
    }
}
