using EbayAlike.Data.Interfaces;
using EbayAlike.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EbayAlike.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        public BaseRepository(DatabaseContext context)
        {
            this.context = context;
        }

        private DbSet<T> Set => this.context.Set<T>();

        private readonly DatabaseContext context;

        public async Task Add(T item)
        {
            Set.Add(item);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, string[] children = null)
        {
            try
            {
                IQueryable<T> query = Set;
                if (children != null)
                {
                    foreach (string entity in children)
                    {
                        query = query.Include(entity);
                    }
                }

                if (filter == null)
                {
                    return query;
                }
                return query.Where(filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Remove(T item)
        {
            this.context.Remove(item);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(T item)
        {
            this.context.Update(item);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}