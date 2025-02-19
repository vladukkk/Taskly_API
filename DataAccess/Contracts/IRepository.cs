
namespace DataAccess.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(Guid Id);

        Task Add(TEntity entity);
        void Attach(TEntity entity);

        Task Delete(Guid Id);
        void Update(TEntity entity);

        Task Save();
    }
}
