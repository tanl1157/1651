using Base.Entity.MappedEntities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Base.DL.DbAccess
{
    public partial class MainContext : DbContext, IApplicationDbContext
    {
        public MainContext()
            : base("name=MainConnection")
        {
        }

        public virtual DbSet<Audience> Audiences { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public IDbConnection Connection => Database.Connection;
    }
}
