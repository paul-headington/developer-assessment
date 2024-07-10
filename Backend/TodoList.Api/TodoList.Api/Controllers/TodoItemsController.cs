using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService _todoItemsService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoItemsService todoItemsService, ILogger<TodoItemsController> logger)
        {
            _todoItemsService = todoItemsService;
            _logger = logger;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var results = await _todoItemsService.GetTodoItemsAsync(false);
            return Ok(results);
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            try
            {
                var result = await _todoItemsService.GetTodoItemAsync(id);
                return Ok(result);
            }catch (ArgumentException ex)
            {
                _logger.LogError(ex, null);
                return  NotFound();
            }
            
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }           

            try
            {
                await _todoItemsService.UpdateTodoItemAsync(id, todoItem);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, null);
                return NotFound();
            }

            
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
        {
            try
            {
                var result = await _todoItemsService.CreateTodoItemAsync(todoItem);
                return CreatedAtAction(nameof(GetTodoItem), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, null);
                return BadRequest(ex.Message);
            }
        }    
    }
}
