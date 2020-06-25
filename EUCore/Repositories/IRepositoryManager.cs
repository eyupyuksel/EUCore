using System;
using EUCore.Entity;

namespace EUCore.Repositories
{
    public interface IRepositoryManager
    {
        IRepository<TEntity> ResolveRepository<TEntity>() where TEntity : class, IEntity<int>, new();
        IRepository<TEntity, TPrimaryKey> ResolveRepository<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>, new();
    }
}
