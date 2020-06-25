using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using EUCore.Configuration;
using EUCore.Entity;
using EUCore.Repositories;

namespace EUCore.UnitofWorks
{
    [SuppressMessage("ReSharper", "PossibleIntendedRethrow")]
    public abstract class UnitofWorkManagerBase : IUnitOfWorkManager
    {
        private readonly AppSettings _appSettings;
        private readonly IComponentContext _context;

        #region .ctor

        private int _call;
        // ReSharper disable once NotAccessedField.Local
        private int _openConnectionCount;
        protected UnitofWorkManagerBase(AppSettings appSettings, IComponentContext context)
        {
            _appSettings = appSettings;
            _context = context;
        }

        #endregion
        public IDbTransaction GetTransaction() => ActiveUnitOfWork?.GetTransaction();
        private IActiveUnitOfWork ActiveUnitOfWork { get; set; }
        protected abstract DbConnection CreateConnection();
        public IDbConnection GetConnection()
        {
            DbConnection connection = null;
            try
            {
                if (ActiveUnitOfWork != null && _call != 0)
                    return ActiveUnitOfWork.GetConnection();

                connection = CreateConnection();
                connection.Disposed += (sender, args) => Console.WriteLine("Connection closed, dbName: {0}", connection.Database);
                connection.ConnectionString = _appSettings.DatabaseConfig.ConnectionString;
                Console.WriteLine("Created new connection, dbName: {0}", connection.Database);



                if (!string.IsNullOrEmpty(connection.ConnectionString))
                {
                    connection.Open();
                    _openConnectionCount++;
                    Console.WriteLine("Connecting opening.. dbName: {0}, hash: {1}", connection.Database,
                        connection.GetHashCode());
                }
                else
                    throw new Exception("ConnectionStringNotFoundException");//(500,"Connecting string not found");

                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(connection?.ConnectionString);
                throw new Exception("Connection Error", ex);
            }
            finally
            {
                if (ActiveUnitOfWork == null && _call == 0)
                {
                    connection?.Close();
                    _openConnectionCount--;
                }
            }
            
        }
        public void BeginTransaction()
        {
            _call++;
            if (ActiveUnitOfWork == null)
            {
                var connection = GetConnection();
                ActiveUnitOfWork = new ActiveUnitOfWork(connection);
            }

            Console.WriteLine("Transaction started, hash: {0}", GetHashCode());
        }

        public void Commit()
        {
            if (ActiveUnitOfWork == null)
                throw new InvalidOperationException("No transaction started.");

            if (_call == 1)
            {
                ActiveUnitOfWork.Commit();
                ActiveUnitOfWork = null;
            }
            _call--;
            Console.WriteLine("Transaction commited, hash: {0}", GetHashCode());
        }

        public void Rollback()
        {
            if (ActiveUnitOfWork == null)
                throw new InvalidOperationException("No transaction started.");
            ActiveUnitOfWork.Rollback();
            ActiveUnitOfWork = null;
            _call = 0;
            Console.WriteLine("Transaction rollbacked, hash: {0}", GetHashCode());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            ActiveUnitOfWork?.Rollback();
            _call = 0;
            Console.WriteLine("UnitofWork has been disposed, Hash : {0}",GetHashCode());
        }
        public IRepository<TEntity> ResolveRepository<TEntity>() where TEntity : class, IEntity<int>, new()
        {
            return _context.Resolve<IRepository<TEntity>>();
        }

        public IRepository<TEntity, TPrimaryKey> ResolveRepository<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            return _context.Resolve<IRepository<TEntity, TPrimaryKey>>();
        }
       
        public bool Exist<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : class, IEntity<TPrimaryKey>, new() where TPrimaryKey : struct
        {
            var record = ResolveRepository<TEntity, TPrimaryKey>().FirstOrDefault(id);
            return record != null;
        }
    }
}
