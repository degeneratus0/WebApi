using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        IFile<DataModel, DataDTO> fileWork;

        public FileController(IFile<DataModel, DataDTO> data)
        {
            fileWork = data;
            fileWork.Set();
        }

        [HttpGet]
        public IEnumerable<DataModel> Get()
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

        /// <response code="204">Item updated</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        /// <response code="204">Item deleted</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
