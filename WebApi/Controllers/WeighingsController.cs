using System;
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
    /// This controller asynchronously handles CRUD interactions with "Weighings" table in the database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeighingsController : ControllerBase
    {
        private readonly IRepository<Weighing> _weighings;
        private readonly IConverter<Weighing, WeighingDTO> _converter;

        /// <summary>
        /// Constructor for WeighingsController
        /// </summary>
        /// <param name="repository">Repository for "Weighings" table access</param>
        /// <param name="converter">Converter for weighings</param>
        public WeighingsController(IRepository<Weighing> repository, IConverter<Weighing, WeighingDTO> converter)
        {
            _weighings = repository;
            _converter = converter;
        }

        /// <summary>
        /// Gets all Weighings
        /// </summary>
        /// <response code="200">All Weighings returned</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<WeighingDTO> Get()
        {
            return _weighings.Entities.Select(_converter.AsDTO);
        }

        /// <summary>
        /// Gets WeighingDTO by specified id
        /// </summary>
        /// <param name="id">Id of the Weighing to look for</param>
        /// <response code="200">WeighingDTO returned</response>
        /// <response code="404">Weighing not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<WeighingDTO>> Get(int id)
        {
            Weighing weighing = await _weighings.ReadAsync(id);
            if (weighing == null)
                return NotFound();
            return _converter.AsDTO(weighing);
        }

        /// <summary>
        /// Adds a Weighing to the database
        /// </summary>
        /// <param name="weighingDTO">Weighing to add</param>
        /// <response code="201">Weighing successfully created</response>
        /// <response code="400">Measure with specified name was not found</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post(WeighingDTO weighingDTO)
        {
            Weighing weighing;
            try
            {
                weighing = _converter.FromDTO(weighingDTO);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            await _weighings.AddAsync(weighing);
            return Ok(weighing);
        }

        /// <summary>
        /// Updates a Weighing with specified id
        /// </summary>
        /// <param name="id">Id of the Weighing to update</param>
        /// <param name="weighingDTO">WeighingDTO for update</param>
        /// <response code="204">Weighing successfully updated</response>
        /// <response code="400">Measure with specified name was not found</response>
        /// <response code="404">Weighing not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, WeighingDTO weighingDTO)
        {
            if (await _weighings.ReadAsync(id) == null)
            {
                return NotFound();
            }
            Weighing weighing;
            try
            {
                weighing = _converter.FromDTO(weighingDTO);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            await _weighings.EditAsync(id, weighing);
            return NoContent();
        }

        /// <summary>
        /// Deletes a Weighing with specified id
        /// </summary>
        /// <param name="id">Id of the Weighing to delete</param>
        /// <response code="204">Weighing successfully deleted</response>
        /// <response code="404">Weighing not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _weighings.ReadAsync(id) == null)
            {
                return NotFound();
            }
            await _weighings.DeleteAsync(id);
            return NoContent();
        }
    }
}
