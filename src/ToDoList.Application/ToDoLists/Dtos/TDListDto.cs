using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.ToDoLists.Dtos
{
    public class TDListDto : EntityDto<Guid>
    {
        public virtual string Title { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual bool Status { get; set; }
        public virtual List<TDItemDto> ToDoItemList{ get; set; }
    }
}
