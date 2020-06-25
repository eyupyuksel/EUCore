using System;
using EUCore.Audit;
using EUCore.Entity;
using EUCore.UnitofWorks;

namespace EUCore.Repositories.Dapper
{
    public class DapperRepository<TEntity> : DapperRepository<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntity<int>, new()
    {
        public DapperRepository(IAuditManager auditManager, IUnitOfWorkManager unitOfWorkManager) : base(auditManager, unitOfWorkManager)
        {
        }
    }
}
