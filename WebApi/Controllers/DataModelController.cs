using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.TestData;

namespace WebApi.Controllers
{
    /// <summary>
    /// This controller handles basic CRUD interactions with Data Models (thread-safely)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataModelController : ControllerBase
    {
        private static List<DataModel> _context = new List<DataModel>();

        /// <summary>
        /// Sets context (erasing existing data)
        /// Removes records with duplicate id
        /// </summary>
        /// <param name="newContext">List of Data Models to set (pass an empty list to set with test data)</param>
        /// <response code="201">List of set Data Models</response>
        /// <response code="400">Provided list is incorrect</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("Set")]
        public IActionResult Set([FromBody] List<DataModel> newContext)
        {
            lock (_context)
            {
                if (newContext.Count == 0)
                {
                    _context = new List<DataModel>(DataModelTestData.TestDataModels);
                }
                else
                {
                    _context = newContext.DistinctBy(x => x.Id).ToList();
                }
                return Created(Url.Action(), _context);
            }
        }

        /// <summary>
        /// Gets all Data Models
        /// </summary>
        /// <response code="200">All Data Models</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<DataModel> Get()
        {
            return _context;
        }

        /// <summary>
        /// Gets content of a Data Model with specified id
        /// </summary>
        /// <param name="id">Id of the Data Model to look for</param>
        /// <response code="200">Data Model's content (string)</response>
        /// <response code="404">Data Model not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            lock (_context)
            {
                DataModel item = _context.FirstOrDefault(x => x.Id == id);
                if (item == null)
                {
                    return NotFound();
                }
                return item.Content;
            }
        }

        /// <summary>
        /// Adds a Data Model to the context
        /// </summary>
        /// <param name="dataModel">Data Model to add</param>
        /// <response code="201">Data Model successfully created</response>
        /// <response code="400">Specified content was incorrect</response>
        /// <response code="409">Data Model with specified id already exists</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public IActionResult Post([FromBody]DataModel dataModel)
        {
            if (dataModel.Content == "")
            {
                return BadRequest();
            }
            lock (_context)
            {
                if (_context.FirstOrDefault(x => x.Id == dataModel.Id) != null)
                {
                    return Conflict();
                }
                _context.Add(dataModel);
            }
            return Created(Url.Action(), dataModel);
        }

        /// <summary>
        /// Updates a Data Model with specified id
        /// </summary>
        /// <param name="id">Id of the Data Model to update</param>
        /// <param name="content">Content for update</param>
        /// <response code="204">Data Model successfully updated</response>
        /// <response code="400">Specified content was incorrect</response>
        /// <response code="404">Data Model not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]string content)
        {
            if (content == "")
            {
                return BadRequest();
            }
            lock (_context)
            {
                DataModel dataModel = _context.FirstOrDefault(x => x.Id == id);
                if (dataModel == null)
                {
                    return NotFound();
                }
                dataModel.Content = content;
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a Data Model with specified id
        /// </summary>
        /// <param name="id">Id of the Data Model to delete</param>
        /// <response code="204">Data Model successfully deleted</response>
        /// <response code="404">Data Model not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            lock(_context)
            {
                DataModel dataModel = _context.FirstOrDefault(x => x.Id == id);
                if (dataModel == null)
                {
                    return NotFound();
                }
                _context.Remove(dataModel);
            }
            return NoContent();
        }
    }
}
