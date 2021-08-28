using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.ToDoLists
{
    public class CreateTDListInput
    {
        public virtual string Title { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual bool Status { get; set; }
    }
}
