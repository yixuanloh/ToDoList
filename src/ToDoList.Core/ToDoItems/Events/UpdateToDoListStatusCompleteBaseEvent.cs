using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.ToDoItems.Events
{
    public class UpdateToDoListStatusCompleteBaseEvent : EventData
    {
        //not used
        public virtual Guid ToDoRefId { get; set; }
    }
}
