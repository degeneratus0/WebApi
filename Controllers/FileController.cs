using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
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

        string path = Directory.GetCurrentDirectory() + "\\storage\\";
        public FileController()
        {
            Directory.CreateDirectory(path);
            if (Directory.GetFiles(path).Length == 0)
            {
                StreamWriter sw1 = new StreamWriter(path + "1.txt", false, System.Text.Encoding.Default);
                sw1.Write("text 1");
                sw1.Close();
                StreamWriter sw2 = new StreamWriter(path + "2.txt", false, System.Text.Encoding.Default);
                sw2.Write("text 2");
                sw2.Close();
                StreamWriter sw3 = new StreamWriter(path + "3.txt", false, System.Text.Encoding.Default);
                sw3.Write("text 3");
                sw3.Close();
            }
        }

        [HttpGet]
        public List<Data> Get()
        {
            List<Data> datas = new List<Data>();
            
            foreach (string s in Directory.GetFiles(path))
            {
                StreamReader sr = new StreamReader(s);
                datas.Add(new Data() { id = Path.GetFileNameWithoutExtension(s), content = sr.ReadToEnd() });
                sr.Close();
            }
            return datas;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            StreamReader sr = new StreamReader(path + $"{id}.txt");
            string s = sr.ReadToEnd();
            sr.Close();
            try
            {
                return s;
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
            StreamWriter sw = new StreamWriter(path + (Directory.GetFiles(path).Length + 1) +".txt", false, System.Text.Encoding.Default);
            sw.Write(item);
            sw.Close();
            return Ok(item);
        }
       
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            StreamReader sr = new StreamReader(path + $"{id}.txt");
            if (sr.ReadToEnd() == null)
            {
                return NotFound();
            }
            sr.Close();
            StreamWriter sw = new StreamWriter(path + $"{id}.txt", false, System.Text.Encoding.Default);
            sw.Write(item);
            sw.Close();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            FileInfo f = new FileInfo(path + $"{id}.txt");
            if(!f.Exists)
            {
                return NotFound();
            }
            f.Delete();
            return NoContent();
        }
    }
}
