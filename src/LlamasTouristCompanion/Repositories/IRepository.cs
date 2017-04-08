using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Repositories
{
    interface IRepository<TEntity, in TPrimaryKey> : IDisposable where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        TEntity GetById(TPrimaryKey id);

        Task<List<TEntity>> GetAllWhere(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TPrimaryKey id);
    }
}
