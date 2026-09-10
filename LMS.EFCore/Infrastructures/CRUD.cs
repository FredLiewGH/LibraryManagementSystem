using LMS.EFCore.Abstractions;
using LMS.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS.EFCore.Infrastructures
{
    public sealed class CRUD<TEntity> : ICRUD<TEntity>        
        where TEntity : class
    {
        private DbSet<TEntity> Set { get; }

        public CRUD(LMSDBContext context)
        {
            ArgumentNullException.ThrowIfNull(context);            
            Set = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(object id, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(id);

            return await Set
                .FindAsync(new[] { id }, token)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken token = default)
        {
            return await Set
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, 
            CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return await Set
                .Where(predicate)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }    

        public async Task<IReadOnlyList<TResult>> SelectAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(selector);

            return await Set
                .Select(selector)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<TResult>> SelectAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentNullException.ThrowIfNull(selector);

            return await Set
                .Where(predicate)
                .Select(selector)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        public async Task AddAsync(TEntity entity, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await Set
                .AddAsync(entity, token)
                .ConfigureAwait(false);
        }

        public Task UpdateAsync(TEntity entity, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Set.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Set.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
