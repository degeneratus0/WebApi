using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        static List<string> context = new List<string>();
        public ModelController()
        {
            if (context.Count == 0)
            {
                context.Add("item1");
                context.Add("item2");
                context.Add("item3");
            } 
        }
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
