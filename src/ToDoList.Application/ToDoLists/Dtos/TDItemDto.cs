using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.ToDoLists.Dtos
{
    public class TDItemDto : EntityDto<Guid>
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Status { get; set; }
        public virtual DateTime TimeToStart { get; set; }
        public virtual Guid ToDoRefId { get; set; }
    }
}
