using IMQ1.ADO.UI.Bll.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMQ1.ADO.UI.Bll
{
    /// <summary>
    /// Handles a set of items
    /// </summary>
    /// <typeparam name="T">Type of entities in the set</typeparam>
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbset;

        public Repository(DbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        /// <summary>
        /// Adds entity to the set
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        /// <summary>
        /// Adds entity list to the set
        /// </summary>
        /// <param name="entities">Entity list to add</param>
        public void AddRange(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
        }

        /// <summary>
        /// Deletes entity from the set
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }

            _dbset.Remove(entity);
        }

        /// <summary>
        /// Deletes entity list from the set
        /// </summary>
        /// <param name="entities">Entity list to delete</param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            var entityList = entities as IList<T> ?? entities.ToList();
            var detached = entityList.Where(entity => _context.Entry(entity).State == EntityState.Detached);
            foreach (var item in detached)
            {
                _dbset.Attach(item);
            }

            _dbset.RemoveRange(entityList);
        }
    }
}
