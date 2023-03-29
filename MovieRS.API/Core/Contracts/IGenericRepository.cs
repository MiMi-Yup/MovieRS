using System.Linq.Expressions;

namespace MovieRS.API.Core.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> FindById<TId>(TId id, bool trackChanges = true);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", bool trackChanges = true);
        Task<bool> Add(TEntity entity);
        Task<bool> Upsert(TEntity entity);
        Task<bool> Delete<TId>(TId id);
        void Delete(TEntity entityToDelete);
    }
}
