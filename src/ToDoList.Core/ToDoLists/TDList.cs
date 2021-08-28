using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.ToDoLists
{

    [Table("ToDoLists")]
    public class TDList : Entity<Guid>
    {
        public virtual string Title { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual bool Status { get; set; }

        public TDList()
        {
            //empty
        }
        public TDList(string title)
        {
            Title = title;
            CreationTime = DateTime.Now;
            Status = false;
        }

        public void UpdateStatus(bool status)
        {
            Status = status;
        }
    }
}
