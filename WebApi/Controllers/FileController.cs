using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Models.TestData;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    /// <summary>
    /// This controller handles basic CRUD interactions with files
    /// </summary>
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository<DataModel, DataModelDTO> _files;

        /// <summary>
        /// Constructor for FileController
        /// </summary>
        /// <param name="repository">Dependency injected repository for file access</param>
        public FileController(IFileRepository<DataModel, DataModelDTO> repository)
        {
            _files = repository;
        }

        /// <summary>
        /// Creates files for every item passed (erases existing data)
        /// </summary>
        /// <param name="datas">List of contents to create files for (pass an empty list to set with test data)</param>
        /// <response code="201">List of set contents</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        [Route("Set")]
        public ActionResult Set([FromBody]List<DataModelDTO> datas)
        {
            List<DataModelDTO> datasToSet;
            if (datas.Count == 0)
            {
                datasToSet = new List<DataModelDTO>(DataModelTestData.TestDataModels.Select(x => new DataModelDTO { Content = x.Content }));
            }
            else
            {
                datasToSet = datas;
            }
            _files.Set(datasToSet);
            return Created(Url.Action(), datas);
        }

        /// <summary>
        /// Erases all files
        /// </summary>
        /// <response code="204">All files were erased</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete]
        [Route("Clear")]
        public ActionResult Clear()
        {
            _files.Clear();
            return NoContent();
        }

        /// <summary>
        /// Gets all files
        /// </summary>
        /// <response code="200">Data Models for all files</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<DataModel> Get()
        {
            return _files.ReadAll();
        }

        /// <summary>
        /// Gets content of the file with specified id
        /// </summary>
        /// <param name="id">Id of the file to look for</param>
        /// <response code="200">File content</response>
        /// <response code="404">File not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<DataModelDTO> Get(string id)
        {
            DataModelDTO result = _files.Read(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        /// <summary>
        /// Creates a file with specified content
        /// </summary>
        /// <param name="item">Content of added file</param>
        /// <response code="201">File successfully created</response>
        /// <response code="400">Specified content was incorrect</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates the file with specified id
        /// </summary>
        /// <param name="id">Id of the file to update</param>
        /// <param name="item">Content for the updated file</param>
        /// <response code="204">File updated</response>  
        /// <response code="400">Specified content was incorrect</response>
        /// <response code="404">File not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes the file with specified id
        /// </summary>
        /// <param name="id">Id of the file to delete</param>
        /// <response code="204">File deleted</response>  
        /// <response code="404">File not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
