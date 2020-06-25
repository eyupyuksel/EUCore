using System;
using EUCore.Repositories;

namespace EUCore.UnitofWorks
{
    public interface IUnitOfWorkManager : IActiveUnitOfWork, IRepositoryManager, IDisposable
    {
        void BeginTransaction();
    }
}
