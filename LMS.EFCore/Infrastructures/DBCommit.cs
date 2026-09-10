using LMS.EFCore.Abstractions;
using LMS.EFCore.Data;

namespace LMS.EFCore.Infrastructures
{
    public sealed class DBCommit : IDBCommit
    {
        private readonly LMSDBContext _context;

        public DBCommit(LMSDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _context.SaveChangesAsync(cancellationToken);
    }
}
