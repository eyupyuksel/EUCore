using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace EUCore.UnitofWorks
{
    public interface IActiveUnitOfWork
    {
        IDbTransaction GetTransaction();
        IDbConnection GetConnection();
        void Commit();
        void Rollback();
    }

    public class ActiveUnitOfWork : IActiveUnitOfWork
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public ActiveUnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _transaction = connection.BeginTransaction();
        }
        public IDbTransaction GetTransaction() => _transaction;
        public IDbConnection GetConnection() => _connection;
        [SuppressMessage("ReSharper", "PossibleIntendedRethrow")]
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("DatabaseException");
            }
            finally
            {
                _connection?.Close();
            }
        }

        [SuppressMessage("ReSharper", "PossibleIntendedRethrow")]
        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw new Exception("DatabaseException");
            }
            finally
            {
                _connection?.Close();
            }
        }
    }
}

