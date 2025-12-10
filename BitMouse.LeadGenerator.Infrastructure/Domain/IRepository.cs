namespace GLeadGenerator.Infrastructure.Domain;

public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    Task InsertAsync(TEntity entity);
}

public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : Entity<int>
{
}

