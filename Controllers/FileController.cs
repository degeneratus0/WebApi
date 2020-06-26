using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        IFile fileWork;
        public FileController(IFile data)
        {
            fileWork = data;
            fileWork.Set();
        }

        [HttpGet]
        public List<Data> Get()
        {
            return fileWork.ReadAll();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            if (fileWork.Read(id) == null)
            {
                return NotFound();
            }
            return fileWork.Read(id);
        }
        
        [HttpPost]
        public ActionResult<string> Post([FromBody] DataDTO item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            fileWork.Add(item);
            return Ok(item);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] DataDTO item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (fileWork.Read(id) == null)
            {
                return NotFound();
            }
            fileWork.Edit(id, item);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (fileWork.Read(id) == null)
            {
                return NotFound();
            }
            fileWork.Delete(id);
            return NoContent();
        }
    }
}
