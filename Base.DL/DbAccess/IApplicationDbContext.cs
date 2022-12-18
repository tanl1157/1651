using Base.Entity.MappedEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.DL.DbAccess
{
    public interface IApplicationDbContext 
    {
        IDbConnection Connection { get; }
        DbSet<Audience> Audiences { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Order> Orders { get; set; }
        Database Database { get; }
        int SaveChanges();

        // entity framework mock interface
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
