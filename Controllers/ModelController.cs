using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        static List<string> context = new List<string> { "item1", "item2", "item3" };
        [HttpGet]
        public List<string> Get()
        {
            return context;
        }
        
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return context[id];
            }
            catch
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        public ActionResult<string> Post([FromBody] string item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            context.Add(item);
            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string item)
        {
            if (context[id] == null)
            {
                return NotFound();
            }
            context[id] = item;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(context[id] == null)
            {
                return NotFound();
            }
            context.RemoveAt(id);
            return NoContent();
        }
    }
}
