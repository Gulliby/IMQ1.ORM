using System;
using System.Linq;
using System.Linq.Expressions;

namespace IMQ1.ADO.UI.DAL.Interface
{
    /// <summary>
    /// Provides methods for working with data
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Searches for entities of specified type
        /// </summary>
        /// <typeparam name="T">Type of entity to search</typeparam>
        /// <param name="navigations">Expressions used to include complex properties</param>
        /// <returns>Entity set for the specified type</returns>
        IQueryable<T> Entity<T>(params Expression<Func<T, object>>[] navigations)
            where T : class;

        /// <summary>
        /// Creates repository for specific type
        /// </summary>
        /// <typeparam name="T">Type to create the repository for</typeparam>
        /// <returns>Repository for the T type</returns>
        IRepository<T> Repository<T>()
            where T : class;

        /// <summary>
        /// Saves changes on the context
        /// </summary>
        void SaveChanges();
    }
}
