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
        interface DataStorage
        {
            static List<string> context = new List<string>();
        }
        //static string path = Directory.GetCurrentDirectory() + "\\storage";
        public ModelController()
        {
            if (DataStorage.context.Count == 0)
            {
                DataStorage.context.Add("item1");
                DataStorage.context.Add("item2");
                DataStorage.context.Add("item3");
            } 
        }
        [HttpGet]
        public List<string> Get()
        {
            return DataStorage.context;
        }
        
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return DataStorage.context[id];
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
            DataStorage.context.Add(item);
            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string item)
        {
            if (DataStorage.context[id] == null)
            {
                return NotFound();
            }
            DataStorage.context[id] = item;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(DataStorage.context[id] == null)
            {
                return NotFound();
            }
            DataStorage.context.RemoveAt(id);
            return NoContent();
        }
    }
}
