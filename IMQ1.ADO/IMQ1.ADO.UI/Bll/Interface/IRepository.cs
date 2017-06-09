using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMQ1.ADO.UI.Bll.Interface
{
    /// <summary>
    /// Handles a set of items
    /// </summary>
    /// <typeparam name="T">Type of containing entity</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Adds entity to the set
        /// </summary>
        /// <param name="entity">Entity to add</param>
        void Add(T entity);

        /// <summary>
        /// Adds entity list to the set
        /// </summary>
        /// <param name="entities">Entity list to add</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Deletes entity from the set
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes entity list from the set
        /// </summary>
        /// <param name="entities">Entity list to delete</param>
        void DeleteRange(IEnumerable<T> entities);
    }
}
