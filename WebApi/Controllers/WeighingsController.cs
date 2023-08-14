using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeighingsController : ControllerBase
    {
        private readonly IRepository<Weighing> _weighings;
        private readonly IConverter<Weighing, WeighingDTO, WeighingDTOid> _converter;

        public WeighingsController(IRepository<Weighing> repository, IConverter<Weighing, WeighingDTO, WeighingDTOid> converter)
        {
            _weighings = repository;
            _converter = converter;
        }

        [HttpGet]
        public IEnumerable<WeighingDTOid> Get()
        {
            return _weighings.ReadAll().Select(_converter.AsDTOid);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Weighing weighing = _weighings.Read(id);
            if (weighing == null)
                return NotFound();
            return Ok(_converter.AsDTO(weighing));
        }

        [HttpPost]
        public IActionResult Post(WeighingDTO weighing)
        {
            try
            {
                _weighings.Add(_converter.FromDTO(weighing));
                return Ok(weighing);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <response code="204">Item updated</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public IActionResult Put(int id, WeighingDTO weighing)
        {
            if (_weighings.Read(id) == null)
            {
                return NotFound();
            }
            try
            {
                _weighings.Edit(id, _converter.FromDTO(weighing));
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <response code="204">Item deleted</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_weighings.Read(id) == null)
            {
                return NotFound();
            }
            _weighings.Delete(id);
            return NoContent();
        }
    }
}
