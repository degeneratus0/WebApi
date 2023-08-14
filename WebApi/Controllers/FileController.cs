using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Models.TestData;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository<DataModel, DataModelDTO> _files;

        public FileController(IFileRepository<DataModel, DataModelDTO> repository)
        {
            _files = repository;
        }

        [HttpPost]
        [Route("Set")]
        public ActionResult Set([FromBody]List<string> stringDatas)
        {
            List<string> stringDatasToSet;
            if (stringDatas.Count == 0)
            {
                stringDatasToSet = new List<string>(DataModelTestData.TestData);
            }
            else
            {
                stringDatasToSet = stringDatas;
            }

            List<DataModelDTO> datas = new List<DataModelDTO>();
            foreach (string stringData in stringDatasToSet)
            {
                datas.Add(new DataModelDTO() { Content = stringData });
            }
            _files.Set(datas);
            return Created(Url.Action(), datas);
        }

        [HttpDelete]
        [Route("Clear")]
        public ActionResult Clear()
        {
            _files.Clear();
            return NoContent();
        }

        [HttpGet]
        public IEnumerable<DataModel> Get()
        {
            return _files.ReadAll();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            string result = _files.Read(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        
        [HttpPost]
        public ActionResult Post([FromBody]DataModelDTO item)
        {
            if (item.Content == null)
            {
                return BadRequest();
            }
            _files.Add(item);
            return Created(Url.Action(), item);
        }

        /// <response code="204">Item updated</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]DataModelDTO item)
        {
            if (item.Content == null)
            {
                return BadRequest();
            }
            if (_files.Read(id) == null)
            {
                return NotFound();
            }
            _files.Edit(id, item);
            return NoContent();
        }

        /// <response code="204">Item deleted</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (_files.Read(id) == null)
            {
                return NotFound();
            }
            _files.Delete(id);
            return NoContent();
        }
    }
}
