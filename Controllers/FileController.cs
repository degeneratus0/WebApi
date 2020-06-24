using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public class Data
        {
            public string id { get; set; }
            public string content { get; set; }
        }

        string path = Directory.GetCurrentDirectory() + "\\storage";
        public FileController()
        {
            Directory.CreateDirectory(path);
            /*
            StreamWriter sw1 = new StreamWriter(path + "\\text1.txt", false, System.Text.Encoding.Default);
            sw1.Write("test text 1");
            sw1.Close();
            StreamWriter sw2 = new StreamWriter(path + "\\text2.txt", false, System.Text.Encoding.Default);
            sw2.Write("test text 2");
            sw2.Close();
            StreamWriter sw3 = new StreamWriter(path + "\\text3.txt", false, System.Text.Encoding.Default);
            sw3.Write("test text 3");
            sw3.Close();
            */
        }

        [HttpGet]
        public List<Data> Get()
        {
            List<Data> datas = new List<Data>();
            
            foreach (string s in Directory.GetFiles(path))
            {
                datas.Add(new Data() { id = s, content = (new StreamReader(s).ReadToEnd()) });
            }
            return datas;
        }

        /*
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
        */
    }
}
