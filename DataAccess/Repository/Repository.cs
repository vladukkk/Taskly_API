using DataAccess.Context;
using DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    internal class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly TaskDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(TaskDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>?> Get(
            Expression<Func<TEntity, bool>>? filter = null
            ,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
            , params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            foreach (var property in includeProperties)
                query = query.Include(property);

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetById(Guid Id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var prop in includeProperties)
                query = query = query.Include(prop);

            return await query.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Attach(TEntity entity)
        {
            _dbSet.Attach(entity);
        }

        public async Task Delete(Guid Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
