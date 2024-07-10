using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoList.Api.Services
{
    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync(bool includeAll);

        Task<TodoItem> GetTodoItemAsync(Guid id);

        Task<TodoItem> CreateTodoItemAsync(TodoItem newTodoItem);

        Task UpdateTodoItemAsync(Guid id, TodoItem updatedTodoItem);
    }

}

