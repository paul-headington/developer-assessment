import { HttpClient } from '@angular/common/http';
import { Injectable, Type } from '@angular/core';
import { Observable } from 'rxjs';
import { Todolist } from '../interface/todolist';

@Injectable({
  providedIn: 'root'
})
export class TodolistService {

  private readonly API_URL = 'https://localhost:5001/api/todoitems'; // should move to a environment settings in future
  constructor(
    private readonly httpClient: HttpClient
  ) { }

  getAllTodolist(): Observable<Todolist[]> {
    return this.httpClient.get<Todolist[]>(this.API_URL);
  }

  getTodolistById(guid: string): Observable<Todolist> {
    return this.httpClient.get<Todolist>(this.API_URL + `?${guid}`);
  }

  createTodolist(body: Todolist): Observable<Todolist> {
    return this.httpClient.post<Todolist>(this.API_URL, body);
  }

  updateTodolist(guid: string, body: Todolist): Observable<Todolist> {
    return this.httpClient.put<Todolist>(this.API_URL + `/${guid}`, body);
  }

  deleteTodolist(guid: string): Observable<Object> {
    return this.httpClient.delete<Object>(this.API_URL + `/${guid}`);
  }
}
