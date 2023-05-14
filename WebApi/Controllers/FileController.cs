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
        private readonly IFile<DataModel, DataModelDTO> fileWork;

        public FileController(IFile<DataModel, DataModelDTO> fileWork)
        {
            this.fileWork = fileWork;
        }

        [HttpPost]
        [Route("Set")]
        public ActionResult Set([FromBody]List<string> stringDatas)
        {
            List<DataModelDTO> datas = new List<DataModelDTO>();
            foreach (string stringData in stringDatas)
            {
                datas.Add(new DataModelDTO() { Content = stringData });
            }
            fileWork.Set(datas);
            return Created(Url.Action(), datas);
        }

        [HttpDelete]
        [Route("Clear")]
        public ActionResult Clear()
        {
            fileWork.Clear();
            return NoContent();
        }

        [HttpGet]
        public IEnumerable<DataModel> Get()
        {
            return fileWork.ReadAll();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            string result = fileWork.Read(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        
        [HttpPost]
        public ActionResult Post([FromBody]DataModelDTO item)
        {
            if (item == null || item.Content == null)
            {
                return BadRequest();
            }
            fileWork.Add(item);
            return Created(Url.Action(), item);
        }

        /// <response code="204">Item updated</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]DataModelDTO item)
        {
            if (item == null || item.Content == null)
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
