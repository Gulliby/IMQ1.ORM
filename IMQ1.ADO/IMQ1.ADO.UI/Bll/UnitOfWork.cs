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

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
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

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
