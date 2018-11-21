using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Models;

namespace Todo.Api.Controllers
{
    [Route("api/todo-items")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly DataContext _dataContex;
        public TodoItemsController(DataContext dataContex)
        {
            _dataContex = dataContex;
        }

        [HttpPost]
        public IActionResult AddItem(Item item)
        {
            try
            {
                _dataContex.Add(item);
                _dataContex.SaveChanges();
                return Ok(item);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            try
            {
                var items = _dataContex.Items.ToList();
                return Ok(items);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            try
            {
                var item = _dataContex.Items.FirstOrDefault(x => x.Id == id);
                return Ok(item);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                var item = _dataContex.Items.FirstOrDefault(x => x.Id == id);
                if (item == null) return null;
                _dataContex.Items.Remove(item);
                _dataContex.SaveChanges();
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Item item)
        {
            try
            {
                if (id != item.Id) return BadRequest("Invalid Data");
                var data = _dataContex.Items.FirstOrDefault(x => x.Id == id);
                if (data == null) return NotFound();
                data.Name = item.Name;
                _dataContex.Items.Update(data);
                _dataContex.SaveChanges();
                return NoContent();
            }
            catch (System.Exception)
            {
                return BadRequest("Error occured");
            }
        }
    }
}