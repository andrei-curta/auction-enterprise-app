
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataMapper.Repository
{
    interface IRepository<T>
    {
        void Insert(T entity);

        void Update(T item);

        void Delete(T entity);

        T GetByID(object id);

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
    }
}
