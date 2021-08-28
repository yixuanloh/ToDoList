using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ToDoItems;

namespace ToDoList.ToDoLists
{
    public class TDListManager : IDomainService
    {
        private readonly ITDListRepository _tDListRepository;
        public TDListManager(ITDListRepository tDListRepository)
        {
            _tDListRepository = tDListRepository;
        }
        //get all todolists
        public IQueryable<TDList> ToDoLists { get { return _tDListRepository.GetAll();  } }

        //create todolist
        public async Task CreateAsync(CreateTDListInput input)
        {
            var toDoList = new TDList(input.Title);
            await _tDListRepository.InsertAsync(toDoList);
        }

        //delete todolist
        public async Task DeleteAsync(Guid id)
        {
            var toDoList = await _tDListRepository.GetAsync(id);
            await _tDListRepository.DeleteAsync(toDoList);
        }

        //update todolist status to true
        public async Task UpdateListAsync(Guid id)
        {
            var toDoList = await _tDListRepository.GetAsync(id);
            toDoList.UpdateStatus(true);

            await _tDListRepository.UpdateAsync(toDoList);
        }

        //update todolist status to false
        public async Task UpdateListStatusFalseAsync(Guid id)
        {

            var toDoList = await _tDListRepository.GetAsync(id);
            toDoList.UpdateStatus(false);

            await _tDListRepository.UpdateAsync(toDoList);
        }
    }
}
