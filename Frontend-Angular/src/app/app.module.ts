import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TodolistModule } from './todolist/todolist.module';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    AppRoutingModule, 
    BrowserModule, 
    HttpClientModule, 
    ReactiveFormsModule,
    FormsModule, 
    ToastrModule.forRoot(),
    TodolistModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
