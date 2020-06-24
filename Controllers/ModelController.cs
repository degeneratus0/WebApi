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
        string path = Directory.GetCurrentDirectory() + "\\storage";

        static List<string> context = new List<string>();
        public ModelController()
        {
            if (context.Count == 0)
                foreach (string s in Directory.GetFiles(path))
                {
                    StreamReader sr = new StreamReader(s); 
                    context.Add(sr.ReadToEnd());
                    sr.Close();
                }
        }
        private void SaveFiles()
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            for (int i = 0; i < context.Count; i++)
            {
                StreamWriter sw = new StreamWriter(path + $"\\{i}.txt", false, System.Text.Encoding.Default);
                sw.Write(context[i]);
                sw.Close();
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
            SaveFiles();
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
            SaveFiles();
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
            SaveFiles();
            return NoContent();
        }
    }
}
