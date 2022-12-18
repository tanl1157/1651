using Base.DL.DbAccess;
using Base.DL.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entity.MappedEntities;
using Base.Entity.ViewModels;
using Dapper;
using System.Data;

namespace Base.DL.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<BookViewModel> GetViewModels()
        {
            return _connection.Query<BookViewModel>("Proc_Book_GetViewModels", null, commandType: CommandType.StoredProcedure).AsList();
        }
    }
}
