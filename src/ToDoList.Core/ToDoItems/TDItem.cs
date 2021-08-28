using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ToDoLists;

namespace ToDoList.ToDoItems
{
    [Table("ToDoItems")]
    public class TDItem : Entity<Guid>
    {
        public virtual string Description { get; set; }
        public virtual bool Status { get; set; }
        public virtual DateTime TimeToStart { get; set; }

        [ForeignKey("ToDoLists")]
        public virtual Guid ToDoRefId { get; set; }
        public virtual TDList ToDoLists { get; set; }

        public TDItem()
        {
            //empty
        }

        public TDItem(string description, DateTime timeToStart, Guid toDoRefId)
        {
            Description = description;
            Status = false;
            TimeToStart = timeToStart;
            ToDoRefId = toDoRefId;
        }
        public void UpdateStatus(bool status)
        {
            Status = status;
        }

    }
}
