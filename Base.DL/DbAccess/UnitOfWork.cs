using Base.DL.IRepository;
using Base.DL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DL.DbAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IAudienceRepository AudienceRepository { get; }
        IOrderRepository OrderRepository { get; }

        void BeginTransaction();
        void Commit();
        int SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public IApplicationDbContext _dbContext { get; }
        private bool _disposed;


        private IBookRepository _bookRepository;
        private IAudienceRepository _audienceRepository;
        private IOrderRepository _orderRepository;

        public UnitOfWork(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBookRepository BookRepository
        {
            get { return _bookRepository ?? (_bookRepository = new BookRepository(_dbContext)); }
        }

        public IAudienceRepository AudienceRepository
        {
            get { return _audienceRepository ?? (_audienceRepository = new AudienceRepository(_dbContext)); }
        }

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new OrderRepository(_dbContext)); }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void BeginTransaction()
        {
            _connection = _dbContext.Connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _dbContext.Database.UseTransaction((DbTransaction)_transaction);
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _connection.Close();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _bookRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
