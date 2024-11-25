using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Data;
using TaskAPI.Models;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static readonly List<TodoItem> _todoItems = [];
        private readonly TodoDbContext _todoDbContext;

        public TaskController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }
        
        //GET api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            //var items = _todoDbContext.TodoItems
            return Ok(_todoItems);
        }
        
        //GET api/tasks/1
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }
        
        //POST api/tasks
        [HttpPost]
        public ActionResult Post([FromBody] TodoItem todoItem)
        {
            _todoItems.Add(todoItem);
            return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
        }
        
        //PUT api/tasks/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            var todoItemTemplate = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItemTemplate == null)
            {
                return NotFound();
            }

            todoItemTemplate.Title = todoItem.Title;
            todoItemTemplate.Description = todoItem.Description;
            todoItemTemplate.IsCompleted = todoItem.IsCompleted;

            return NoContent();
        }
        
        // DELETE api/tasks/1
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todoItemToDelete = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItemToDelete == null)
            {
                return NotFound();
            }

            _todoItems.Remove(todoItemToDelete);
            
            return NoContent();
        }




    }
}
