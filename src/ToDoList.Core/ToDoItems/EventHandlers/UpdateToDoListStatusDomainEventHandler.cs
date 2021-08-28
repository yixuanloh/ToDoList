using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Abp.Threading;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ToDoItems.Events;
using ToDoList.ToDoLists;

namespace ToDoList.ToDoItems.EventHandlers
{
    public class UpdateToDoListStatusDomainEventHandler : IEventHandler<UpdateToDoListStatusCompleteBaseEvent>,
        IEventHandler<UpdateToDoListStatusCreateBaseEvent>, IEventHandler<UpdateToDoListStatusDeleteBaseEvent>, ITransientDependency
    {
        //not used
        private readonly TDListManager _tDListManager;
        private readonly TDItemManager _tDItemManager;

        public UpdateToDoListStatusDomainEventHandler(TDListManager tDListManager, TDItemManager tDItemManager)
        {
            _tDListManager = tDListManager;
            _tDItemManager = tDItemManager;
        }

        //event that update todolist status to true if all todolist item are completed
        public void HandleEvent(UpdateToDoListStatusCompleteBaseEvent eventData)
        {
            var toDoItem = _tDItemManager.ToDoItems.Where(x => x.ToDoRefId == eventData.ToDoRefId).ToList();

            if(!toDoItem.Any(x => !x.Status))
            {
                AsyncHelper.RunSync(() => _tDListManager.UpdateListAsync(eventData.ToDoRefId));
            }
        }

        //event that update todolist status to false when new todolist item are created
        public void HandleEvent(UpdateToDoListStatusCreateBaseEvent eventData)
        {
            var toDoItem = _tDItemManager.ToDoItems.Where(x => x.ToDoRefId == eventData.ToDoRefId).ToList();
            
            if(toDoItem.Any(x => x.Status))
            {
                AsyncHelper.RunSync(() => _tDListManager.UpdateListStatusFalseAsync(eventData.ToDoRefId));
            }
        }

        //event that update todolist status based on deletion
        //if the leftover todolist item are completed after the deletion of certain item, then todolist status will be updated to true
        //if the leftover todolist item involving completed and incomplete after the deletion of certain item, then todolist status will be updated to false
        public void HandleEvent(UpdateToDoListStatusDeleteBaseEvent eventData)
        {
            var toDoItem = _tDItemManager.ToDoItems.Where(x => x.ToDoRefId == eventData.ToDoRefId).ToList();
            var filteredToDoItem = toDoItem.Where(x => x.Id != eventData.Id).ToList();

            if (!filteredToDoItem.Any(x => !x.Status))
            {
                AsyncHelper.RunSync(() => _tDListManager.UpdateListAsync(eventData.ToDoRefId));
            }
            else
            {
                AsyncHelper.RunSync(() => _tDListManager.UpdateListStatusFalseAsync(eventData.ToDoRefId));
            }

            if(!filteredToDoItem.Any())
            {
                AsyncHelper.RunSync(() => _tDListManager.UpdateListStatusFalseAsync(eventData.ToDoRefId));

            }
        }

    }
}
