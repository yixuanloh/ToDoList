using Abp.Domain.Services;
using Abp.Events.Bus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ToDoItems.Events;

namespace ToDoList.ToDoItems
{
    public class TDItemManager : IDomainService
    {
        private readonly ITDItemRepository _tDItemRepository;
        public IEventBus EventBus { get; set; }

        public TDItemManager(ITDItemRepository tDItemRepository)
        {
            _tDItemRepository = tDItemRepository;
            EventBus = NullEventBus.Instance;
        }

        //get all todolist item
        public IQueryable<TDItem> ToDoItems { get { return _tDItemRepository.GetAll(); } }

        //create todolist item
        public async Task CreateItemAsync(CreateTDItemInput input)
        {
            var toDoItem = new TDItem(input.Description, input.TimeToStart, input.ToDoRefId);
            await _tDItemRepository.InsertAsync(toDoItem);
            EventBus.Trigger(new UpdateToDoListStatusCreateBaseEvent { ToDoRefId = toDoItem.ToDoRefId });

        }

        //delete todolist item
        public async Task DeleteItemAsync(Guid id)
        {
            var toDoItem = await _tDItemRepository.GetAsync(id);
            await _tDItemRepository.DeleteAsync(toDoItem);
            EventBus.Trigger(new UpdateToDoListStatusDeleteBaseEvent { ToDoRefId = toDoItem.ToDoRefId, Id = toDoItem.Id });
        }

        //delete all todolist item if the todolist is deleted
        public async Task DeleteAllItemAsync(Guid id)
        {
            var toDoItem = await _tDItemRepository.GetAll().Where(x => x.ToDoRefId == id).ToListAsync();
            toDoItem.Select(x => _tDItemRepository.DeleteAsync(x));
        }

        //update todolist item status to true
        public async Task UpdateItemAsync(Guid id)
        {
            var toDoItem = await _tDItemRepository.GetAsync(id);
            toDoItem.UpdateStatus(true);

            await _tDItemRepository.UpdateAsync(toDoItem);
            EventBus.Trigger(new UpdateToDoListStatusCompleteBaseEvent { ToDoRefId = toDoItem.ToDoRefId });
            
        }



    }
}
