import { Component, Injector, OnInit } from '@angular/core';
import { TDListDto, TDListServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.css']
})
export class TodolistComponent extends AppComponentBase implements OnInit {

  ToDoListDto: TDListDto = new TDListDto();
  ToDoDtoList: TDListDto[];

  constructor(injector: Injector,
    private _tDListServiceProxy: TDListServiceProxy,
    ) { 
      super(injector);
    }

  ngOnInit(): void {
    this.getAll();
  }

  //get all todolist
  getAll(): void {
    abp.ui.setBusy();
    this._tDListServiceProxy.getAll().subscribe((res) => {
      this.ToDoDtoList = res;
      abp.ui.clearBusy();
    })
  }
  
  //create new todolist
  createToDoList() : void {
    if(Object.keys(this.ToDoListDto).length === 0) {
      this.notify.error('Please do not leave the field empty!')
    }
    else {
      this._tDListServiceProxy.create(this.ToDoListDto).subscribe(() => {
        this.notify.info('Created Successfully');
        this.getAll();
        this.ToDoListDto = new TDListDto();
      })
    }
  }

  //delete todolist
  deleteToDoList(id) : void {
    this._tDListServiceProxy.delete(id).subscribe(() => {
      this.notify.info('Deleted Successfully');
      this.getAll();
    })
  }

}
