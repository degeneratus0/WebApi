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
        interface DataWork
        {
            static string path = Directory.GetCurrentDirectory() + "\\storage\\";
            static void Set()
            {
                Directory.CreateDirectory(path);
                if (Directory.GetFiles(path).Length == 0)
                {
                    Add("text 1");
                    Add("text 2");
                    Add("text 3");
                }
            }
            static string Read(string id)
            {
                try
                {
                    StreamReader sr = new StreamReader(path + id + ".txt");
                    string s = sr.ReadToEnd();
                    sr.Close();
                    return s;
                }
                catch
                {
                    return null;
                }
            }
            static List<Data> ReadAll()
            {
                List<Data> datas = new List<Data>();
                foreach (string s in Directory.GetFiles(path))
                {
                    datas.Add(new Data() { 
                        id = Path.GetFileNameWithoutExtension(s), 
                        content = Read(Path.GetFileNameWithoutExtension(s)) 
                    });
                }
                return datas;
            }
            static void Add(string content)
            {
                StreamWriter sw = new StreamWriter(path + (Directory.GetFiles(path).Length + 1) + ".txt", false, System.Text.Encoding.Default);
                sw.Write(content);
                sw.Close();
            }
            static void Edit(string id, string content)
            {
                StreamWriter sw = new StreamWriter(path + id + ".txt", false, System.Text.Encoding.Default);
                sw.Write(content);
                sw.Close();
            }
            static void Delete(string id)
            {
                FileInfo f = new FileInfo(path + id + ".txt");
                f.Delete();
            }
        }
        public class Data
        {
            public string id { get; set; }
            public string content { get; set; }
        }
        
        public FileController()
        {
            DataWork.Set();
        }

        [HttpGet]
        public List<Data> Get()
        {
            return DataWork.ReadAll();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            if (DataWork.Read(id) == null)
            {
                return NotFound();
            }
            return DataWork.Read(id);
        }
        
        [HttpPost]
        public ActionResult<string> Post([FromBody] string item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            DataWork.Add(item);
            return Ok(item);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] string item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (DataWork.Read(id) == null)
            {
                return NotFound();
            }
            DataWork.Edit(id, item);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (DataWork.Read(id) == null)
            {
                return NotFound();
            }
            DataWork.Delete(id);
            return NoContent();
        }
    }
}
