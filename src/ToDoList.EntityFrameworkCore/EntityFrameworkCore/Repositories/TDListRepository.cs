using Abp.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ToDoLists;

namespace ToDoList.EntityFrameworkCore.Repositories
{
    public class TDListRepository : ToDoListRepositoryBase<TDList, Guid>, ITDListRepository
    {
        public TDListRepository(IDbContextProvider<ToDoListDbContext> dbContextProvider) : base(dbContextProvider)
        {
            //empty
        }
    }
}
