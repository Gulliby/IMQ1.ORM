using IMQ1.ADO.UI.Bll.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMQ1.ADO.UI.Bll
{
    /// <summary>
    /// Provides methods for working with underlying data context
    /// </summary>
    /// <typeparam name="TContext">Data context to work with</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly DbContext _context;
        private bool _disposed;

        public UnitOfWork(TContext context)
        {
            _context = context;
            ConfigureContext(_context);
        }

        /// <summary>
        /// Searches for entities of specified type
        /// </summary>
        /// <typeparam name="T">Type of entity to search</typeparam>
        /// <param name="navigations">Expressions used to include complex properties</param>
        /// <returns>Entity set for the specified type</returns>
        public IQueryable<T> Entity<T>(params Expression<Func<T, object>>[] navigations)
            where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            if (navigations == null || navigations.Length == 0)
            {
                return query;
            }

            return navigations.Aggregate(query, (current, navigation) => current.Include(navigation));
        }

        /// <summary>
        /// Creates repository for specific type
        /// </summary>
        /// <typeparam name="TItem">Type to create the repository for</typeparam>
        /// <returns>Repository for the TItem type</returns>
        public IRepository<TItem> Repository<TItem>()
            where TItem : class
        {
            return new Repository<TItem>(_context);
        }

        /// <summary>
        /// Saves changes on the context
        /// </summary>
        public void SaveChanges()
        {
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();
        }

        /// <summary>
        /// Disposes the instance
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_context != null && disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        private void ConfigureContext(DbContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
        }
    }
}
