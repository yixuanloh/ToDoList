using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.ToDoItems
{
    public class CreateTDItemInput
    {
        public virtual string Description { get; set; }
        public virtual bool Status { get; set; }
        public virtual DateTime TimeToStart { get; set; }
        public virtual Guid ToDoRefId { get; set; }
    }
}
