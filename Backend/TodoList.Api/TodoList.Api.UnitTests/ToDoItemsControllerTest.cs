using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Api.Controllers;
using Xunit;
using TodoList.Api.Services;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace TodoList.Api.UnitTests
{
    public class ToDoItemsControllerTest
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoItemsService _todoItemsService;
        private readonly TodoContext _context;
        private readonly TodoItemsController _controller;
        

        public ToDoItemsControllerTest()
        {
            var options = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase(databaseName: "TodoItemsDB").Options;
            _context = new TodoContext(options);
            _todoItemsService = new TodoItemsService(_context);
            _controller = new TodoItemsController(_todoItemsService, _logger);


            // Mock some data
            _context.TodoItems.AddRange(new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid(), Description = "description 1", IsCompleted = false },
                new TodoItem { Id = Guid.NewGuid(), Description = "description 2", IsCompleted = false }
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllTodoItems()
        {
            var result = await _controller.GetTodoItems();
            var oKResult = Assert.IsType<OkObjectResult>(result);

            var items = Assert.IsType<List<TodoItem>>(oKResult.Value);
            Assert.Equal(2, items.Count);

        }

        [Fact]
        public async Task PostTodoItem()
        {
            var newItem = new TodoItem { Description = "New todo", IsCompleted = false };
            var result = await _controller.PostTodoItem(newItem);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var item = Assert.IsType<TodoItem>(createdAtActionResult.Value);
            Assert.Equal("New todo", item.Description);
            Assert.False(item.IsCompleted);
        }

    }
}
