namespace ToDoList.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{

        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
        {
            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetToDoItems), new { id = toDoItem.Id }, toDoItem);
        }

        // PUT: api/todo/5/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> UpdateTodoCompletion(int id, [FromBody] bool isCompleted)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/todo/5
        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteToDoItem(int id)
            {
                var toDoItem = await _context.ToDoItems.FindAsync(id);
                if (toDoItem == null)
                {
                    return NotFound();
                }

                _context.ToDoItems.Remove(toDoItem);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool ToDoItemExists(int id)
            {
                return _context.ToDoItems.Any(e => e.Id == id);
            }
}

