namespace LMS.EFCore.Abstractions
{
    public interface ICRUD<TEntity> : IRead<TEntity>, IWrite<TEntity> where TEntity : class
    {
    }
}
