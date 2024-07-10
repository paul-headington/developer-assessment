using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("TodoList.Api.UnitTests")]
namespace TodoList.Api.Services
{
    internal class TodoItemsService : ITodoItemsService
    {
        private readonly TodoContext _context;

        public TodoItemsService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync(bool includeAll)
        {
            var result = await TodoItemsQuery(includeAll).ToListAsync();
            return result;
        }

        public async Task<TodoItem> GetTodoItemAsync(Guid id)
        {
            CheckForValidId(id);

            var result = await _context.TodoItems.FindAsync(id)
                         ?? throw new ArgumentException($"Unable to find item with ID: {id}");

            return result;
        }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItem newTodoItem)
        {
            CheckForDescription(newTodoItem.Description);
            await CheckDescriptionExists(newTodoItem.Description);

            _context.TodoItems.Add(newTodoItem);
            await _context.SaveChangesAsync();

            return newTodoItem;
        }

        public async Task UpdateTodoItemAsync(Guid id, TodoItem updatedTodoItem)
        {
            CheckForValidId(id);
            CheckForDescription(updatedTodoItem.Description);

            var todoItem = await GetTodoItemAsync(id);
            todoItem.Description = updatedTodoItem.Description;
            todoItem.Title = updatedTodoItem.Title;
            todoItem.IsCompleted = updatedTodoItem.IsCompleted;

            await _context.SaveChangesAsync();
        }

        private IQueryable<TodoItem> TodoItemsQuery(bool includeAll) => _context.TodoItems.Where(x => includeAll || !x.IsCompleted);

        private static void CheckForValidId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID is required", nameof(id));
        }

        private void CheckForDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required");
        }

        private async Task CheckDescriptionExists(string description)
        {
            var exists = await TodoItemsQuery(false).AnyAsync(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant());
            if (exists)
                throw new ArgumentException("Description already exists");
        }

    }
}

