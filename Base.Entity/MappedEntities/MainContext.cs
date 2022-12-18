using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Base.Entity.MappedEntities
{
    public partial class MainContext : DbContext
    {
        public MainContext()
            : base("name=MainContext")
        {
        }

        public virtual DbSet<Audience> Audiences { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
