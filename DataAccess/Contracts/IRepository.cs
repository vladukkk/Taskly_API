
using System.Linq.Expressions;

namespace DataAccess.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>?> Get(
            Expression<Func<TEntity,bool>>? filter = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
            , params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity?> GetById(Guid Id
            , params Expression<Func<TEntity, object>>[] includeProperties);

        Task Add(TEntity entity);
        void Attach(TEntity entity);

        Task Delete(Guid Id);
        void Update(TEntity entity);

        Task SaveAsync();
    }
}
