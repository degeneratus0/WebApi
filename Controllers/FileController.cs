using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;


namespace WebApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        IData dataWork;
        public FileController(IData data)
        {
            dataWork = data;
            dataWork.Set();
        }

        [HttpGet]
        public List<IData.Data> Get()
        {
            return dataWork.ReadAll();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            if (dataWork.Read(id) == null)
            {
                return NotFound();
            }
            return dataWork.Read(id);
        }
        
        [HttpPost]
        public ActionResult<string> Post([FromBody] string item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            dataWork.Add(item);
            return Ok(item);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] string item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (dataWork.Read(id) == null)
            {
                return NotFound();
            }
            dataWork.Edit(id, item);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (dataWork.Read(id) == null)
            {
                return NotFound();
            }
            dataWork.Delete(id);
            return NoContent();
        }
    }
}
