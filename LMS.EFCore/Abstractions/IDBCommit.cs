namespace LMS.EFCore.Abstractions
{
    public interface IDBCommit
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
