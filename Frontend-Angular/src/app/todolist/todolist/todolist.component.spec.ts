import { ComponentFixture, TestBed, fakeAsync } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TodolistService } from '../service/todolist.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { TodolistComponent } from './todolist.component';
import { of } from 'rxjs';

describe('TodolistComponent', () => {
  let component: TodolistComponent;
  let fixture: ComponentFixture<TodolistComponent>;
  let todolistService: TodolistService;
  let toastrService: ToastrService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TodolistComponent ],
      imports: [HttpClientTestingModule, ToastrModule.forRoot(), FormsModule], 
      providers: [
        TodolistService, 
        ToastrService
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TodolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getTodoItems method after called getItems', fakeAsync(() => {
    spyOn(todolistService, 'getAllTodolist').and.returnValue(of([
        { id: "1233", isCompleted: false, title: "test", description: "Test item"},
        { id: "1234", isCompleted: true, title: "test",  description: "Test item 2"}
      ]));
    component.getTodolist();
    expect(todolistService.getAllTodolist).toHaveBeenCalled();
    fixture.whenStable().then(() => {
      component.todolist$.subscribe(value => {
        expect(value.length).toBe(2);
     });
    });
  }));

});
