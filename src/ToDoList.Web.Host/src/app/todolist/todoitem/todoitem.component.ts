import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { TDListDto, TDListServiceProxy, TDItemDto } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
import 'push.js';
import { interval, Observable, Subscription } from 'rxjs';


@Component({
  selector: 'app-todoitem',
  templateUrl: './todoitem.component.html',
  styleUrls: ['../todolist.component.css']
})
export class TodoitemComponent extends AppComponentBase implements OnInit {

  ToDoItemDto: TDItemDto = new TDItemDto();
  ToDoItemDtoList: TDItemDto[];
  todolistId: string;
  subscription: Subscription;
  date;

  constructor(
    injector: Injector,
    private _activatedRoute: ActivatedRoute,
    private _tDListServiceProxy: TDListServiceProxy,
  ) {
    super(injector)
  }

  ngOnInit(): void {
    var myDate = new Date();
    this.date = moment(myDate).format();
    this._activatedRoute.params.subscribe((params) => {
      this.todolistId = params['id'];
    })
 
    this.get();
    let source = interval(1000);
    this.subscription = source.subscribe(x => {
      this.notificationTimer();
    })
  }

  //get all todolist item for a certain todolist
  get(): void {
    abp.ui.setBusy();
    this._tDListServiceProxy.get(this.todolistId).subscribe((res) => {
      this.ToDoItemDtoList = res;
      console.log(res);
      abp.ui.clearBusy();
    })
  }

  //create new todolist item
  createToDoItem(): void {
    this.ToDoItemDto.toDoRefId = this.todolistId;
    this.ToDoItemDto.timeToStart = moment(this.ToDoItemDto.timeToStart).add(8, 'hours');
    this._tDListServiceProxy.createItem(this.ToDoItemDto).subscribe(() => {
      this.notify.info('Created Successfully');
      this.get();
      this.ToDoItemDto = new TDItemDto();
    })
  }

  //delete todolist item
  deleteToDoItem(id): void {
    this._tDListServiceProxy.deleteItem(id).subscribe(() => {
      this.notify.info('Deleted Successfully');
      this.get();
    })
  }

  //update todolist item status
  updateToDoItemStatus(id): void {
    this._tDListServiceProxy.updateItem(id).subscribe(() => {
      this.notify.info('Status Updated Successfully');
      this.get();
    })
  }

  //notification function
  notificationTimer(): void {
    var myDate = new Date();
    if (this.ToDoItemDtoList != null) {
      for (var i = 0; i < this.ToDoItemDtoList.length; i++) {  
        if (moment(this.ToDoItemDtoList[i].timeToStart).isSame(moment(myDate).format()) && this.ToDoItemDtoList[i].status == false) {
          Push.create("There's an item to be done!", {
            body: this.ToDoItemDtoList[i].description,
            timeout: 4000,
            onClick: function () {
                window.focus();
                this.close();
            }
        });
        }
      }
    }
  }

}
