using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    /// <summary>
    /// This controller asynchronously handles CRUD interactions with "Measures" table in the database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MeasuresController : ControllerBase
    {
        private readonly IRepository<Measure> _measures;
        private readonly IConverter<Measure, MeasureDTO> _converter;

        /// <summary>
        /// Constructor for MeasuresController
        /// </summary>
        /// <param name="repository">Repository for "Measures" table access</param>
        /// <param name="converter">Converter for measures</param>
        public MeasuresController(IRepository<Measure> repository, IConverter<Measure, MeasureDTO> converter)
        {
            _measures = repository;
            _converter = converter;
        }

        /// <summary>
        /// Gets all Measures
        /// </summary>
        /// <response code="200">All Measures returned</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<MeasureDTO> Get()
        {
            return _measures.Entities.Select(_converter.AsDTO);
        }

        /// <summary>
        /// Gets MeasureDTO by specified id
        /// </summary>
        /// <param name="id">Id of the Measure to look for</param>
        /// <response code="200">MeasureDTO returned</response>
        /// <response code="404">Measure not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasureDTO>> Get(int id)
        {
            Measure measure = await _measures.ReadAsync(id);
            if (measure == null)
                return NotFound();
            return _converter.AsDTO(measure);
        }

        /// <summary>
        /// Adds a Measure to the database
        /// </summary>
        /// <param name="measure">Measure to add</param>
        /// <response code="201">Measure successfully created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MeasureDTO measure)
        {
            await _measures.AddAsync(_converter.FromDTO(measure));
            return Ok(measure);
        }

        /// <summary>
        /// Updates a Measure with specified id
        /// </summary>
        /// <param name="id">Id of the Measure to update</param>
        /// <param name="measure">MeasureDTO for update</param>
        /// <response code="204">Measure successfully updated</response>
        /// <response code="404">Measure not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MeasureDTO measure)
        {
            if (await _measures.ReadAsync(id) == null)
            {
                return NotFound();
            }
            await _measures.EditAsync(id, _converter.FromDTO(measure));
            return NoContent();
        }

        /// <summary>
        /// Deletes a Measure with specified id
        /// </summary>
        /// <param name="id">Id of the Measure to delete</param>
        /// <response code="204">Measure successfully deleted</response>
        /// <response code="404">Measure not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _measures.ReadAsync(id) == null)
            {
                return NotFound();
            }
            await _measures.DeleteAsync(id);
            return NoContent();
        }
    }
}
