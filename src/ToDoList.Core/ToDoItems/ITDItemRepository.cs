using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.ToDoItems
{
    public interface ITDItemRepository : IRepository<TDItem, Guid>
    {
        //empty
    }
}
