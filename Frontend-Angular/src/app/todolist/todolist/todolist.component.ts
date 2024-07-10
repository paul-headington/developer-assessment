import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Todolist } from '../interface/todolist';
import { TodolistService } from '../service/todolist.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.css']
})
export class TodolistComponent implements OnInit {
  todolist$?: Observable<Todolist[]>; 
  description: string = '';
  title: string = '';
  // could update to be an instance of the interface instead of individual vars

  public constructor(
    private readonly todolistService: TodolistService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.getTodolist();
  }

  getTodolist() {
    this.todolist$ = this.todolistService.getAllTodolist();
  }

  handleAdd() {
    const newToDo: Todolist = { title: this.title, description: this.description, isCompleted: false };
    this.todolistService.createTodolist(newToDo).subscribe(response => {
      console.log('create response', response); 
      this.toastrService.success('Success', 'Success Create!');
      this.handleClear()
      this.getTodolist()
    }, (err: HttpErrorResponse) => {
      this.toastrService.error('Failed', `${err.error}`);
      throw new Error(err.error);
    })
  }

  handleClear() {
    this.title = '';
    this.description = '';
  }

  handleMarkAsComplete(item: Todolist) {
    item.isCompleted = true;
    this.todolistService.updateTodolist(item.id, item).subscribe(response => {
      this.toastrService.success('Completed', 'Task Completed');
      this.getTodolist()
    }, (err: HttpErrorResponse) => {
      this.toastrService.error('Failed', 'Failed Update!');
      throw new Error(err.error);
    })
  }
}
