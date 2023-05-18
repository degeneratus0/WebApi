using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Models.TestData;

namespace WebApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository<DataModel, DataModelDTO> fileRepository;

        public FileController(IFileRepository<DataModel, DataModelDTO> fileWork)
        {
            this.fileRepository = fileWork;
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
            fileRepository.Set(datas);
            return Created(Url.Action(), datas);
        }

        [HttpDelete]
        [Route("Clear")]
        public ActionResult Clear()
        {
            fileRepository.Clear();
            return NoContent();
        }

        [HttpGet]
        public IEnumerable<DataModel> Get()
        {
            return fileRepository.ReadAll();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            string result = fileRepository.Read(id);
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
            fileRepository.Add(item);
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
            if (fileRepository.Read(id) == null)
            {
                return NotFound();
            }
            fileRepository.Edit(id, item);
            return NoContent();
        }

        /// <response code="204">Item deleted</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (fileRepository.Read(id) == null)
            {
                return NotFound();
            }
            fileRepository.Delete(id);
            return NoContent();
        }
    }
}
