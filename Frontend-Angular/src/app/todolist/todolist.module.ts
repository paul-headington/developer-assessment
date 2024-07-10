import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodolistComponent } from './todolist/todolist.component';



@NgModule({
  declarations: [
    TodolistComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class TodolistModule { }
