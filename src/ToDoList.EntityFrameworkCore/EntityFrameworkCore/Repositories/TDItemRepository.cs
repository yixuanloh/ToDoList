using Abp.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ToDoItems;

namespace ToDoList.EntityFrameworkCore.Repositories
{
    public class TDItemRepository : ToDoListRepositoryBase<TDItem, Guid>, ITDItemRepository
    {
        public TDItemRepository(IDbContextProvider<ToDoListDbContext> dbContextProvider) : base(dbContextProvider)
        {
            //empty
        }
    }
}
