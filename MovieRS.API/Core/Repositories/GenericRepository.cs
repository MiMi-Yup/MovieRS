using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using MovieRS.API.Models;
using MovieRS.API.Core.Contracts;

namespace MovieRS.API.Core.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal MovieRsContext _context;
        internal DbSet<TEntity> dbSet;
        public readonly ILogger _logger;
        public readonly IMapper _mapper;

        public GenericRepository(MovieRsContext context, ILogger logger, IMapper mapper)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
            _logger = logger;
            _mapper = mapper;
        }


        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> FindById<TId>(TId id, bool trackChanges = true)
        {
            var item = await dbSet.FindAsync(id);

            if (item != null && trackChanges)
            {
                _context.Entry(item).State = EntityState.Detached;
            }
            return item;
        }


        public virtual IQueryable<TEntity> Find(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "", bool trackChanges = true)
        {
            IQueryable<TEntity> query = trackChanges ? dbSet : dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Delete<TId>(TId id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
                return true;
            }
            return false;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(params TEntity[] entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.AttachRange(entityToDelete);
            }
            dbSet.RemoveRange(entityToDelete);
        }

        public async virtual Task<IEnumerable<TEntity>> All()
        {
            return await dbSet.ToListAsync();
        }

        public virtual Task<bool> Upsert(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
