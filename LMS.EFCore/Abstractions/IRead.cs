using System.Linq.Expressions;

namespace LMS.EFCore.Abstractions
{
    public interface IRead<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TResult>> SelectAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TResult>> SelectAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            CancellationToken cancellationToken = default);
    }
}
