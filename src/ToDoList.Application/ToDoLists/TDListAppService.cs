using Abp.Collections.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ToDoItems;
using ToDoList.ToDoLists.Dtos;
using Abp.Linq.Extensions;
using Abp.UI;
using Abp.Events.Bus;
using ToDoList.ToDoItems.Events;

namespace ToDoList.ToDoLists
{
    public class TDListAppService : ToDoListAppServiceBase
    {
        private readonly TDListManager _tDListManager;
        private readonly TDItemManager _tDItemManager;

        public TDListAppService(TDListManager tDListManager, TDItemManager tDItemManager)
        {
            _tDListManager = tDListManager;
            _tDItemManager = tDItemManager;

        }

        //get all todolist by calling the function in manager
        public async Task<List<TDListDto>> GetAll()
        {
            var todolist = await _tDListManager.ToDoLists.ToListAsync();

            var toDoListItem = todolist
             .GroupJoin(_tDItemManager.ToDoItems, x => x.Id, y => y.ToDoRefId, (x, y) => new
             {
                ToDoList = x,
                ToDoItems = y
             }).ToList();


            var output = toDoListItem.Select(x => new TDListDto
            {
                Id = x.ToDoList.Id,
                Title = x.ToDoList.Title,
                CreationTime = x.ToDoList.CreationTime,
                Status = x.ToDoList.Status,
                ToDoItemList = x.ToDoItems.Select(y => new TDItemDto
                {
                    Id = y.Id,
                    Title = x.ToDoList.Title,
                    Description = y.Description,
                    Status = y.Status,
                    TimeToStart = y.TimeToStart,
                    ToDoRefId = (Guid)y.ToDoRefId
                }).ToList()
            }).ToList();

            return output;
           
        }

        //get all todolist item by calling the function in manager
        public async Task<List<TDItemDto>>Get(Guid id)
        {
            var todolist = await _tDListManager.ToDoLists.FirstOrDefaultAsync(x => x.Id == id);

            if (todolist == null)
            {
                throw new UserFriendlyException("To-Do List not found");
            }

            var todolistitem = await _tDItemManager.ToDoItems.Where(x => x.ToDoRefId == todolist.Id).ToListAsync();

            var output = todolistitem.Select(x => new TDItemDto
            {
                Id = x.Id,
                Title = todolist.Title,
                Description = x.Description,
                Status = x.Status,
                TimeToStart = x.TimeToStart,
                ToDoRefId = (Guid)x.ToDoRefId
            }).ToList();

            return output;
        }

        //create new todolist by calling the function in manager
        public async Task CreateAsync(TDListDto input)
        {
            var toDoList = new CreateTDListInput
            {
                Title = input.Title,
                CreationTime = DateTime.Now,
                Status = false
            };

            await _tDListManager.CreateAsync(toDoList);
        }

        //create new todolist item by calling the function in manager
        public async Task CreateItemAsync(TDItemDto input)
        {
            var toDoItem = new CreateTDItemInput
            {
                Description = input.Description,
                Status = false,
                TimeToStart = input.TimeToStart,
                ToDoRefId = input.ToDoRefId
            };

            await _tDItemManager.CreateItemAsync(toDoItem);
        }

        //delete todolist by calling the function in manager
        public async Task DeleteAsync(Guid id)
        {
            var toDoItem = await _tDItemManager.ToDoItems.Where(x => x.ToDoRefId == id).ToListAsync();
            bool isEmpty = !toDoItem.Any();

            if(isEmpty)
            {
                await _tDListManager.DeleteAsync(id);
            }
            else
            {
                await _tDItemManager.DeleteAllItemAsync(id);
                await _tDListManager.DeleteAsync(id);
            }

        }

        //delete todolist item by calling the function in manager
        public async Task DeleteItemAsync(Guid id)
        {
            await _tDItemManager.DeleteItemAsync(id);
        }

        //update todolist item status by calling the function in manager
        public async Task UpdateItemAsync(Guid id)
        {
            await _tDItemManager.UpdateItemAsync(id);
        }
    }
}
