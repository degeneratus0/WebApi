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
    public class MeasuresController : ControllerBase
    {
        private readonly IRepository<Measure> _measures;
        private readonly IConverter<Measure, MeasureDTO, MeasureDTOid> _converter;

        public MeasuresController(IRepository<Measure> repository, IConverter<Measure, MeasureDTO, MeasureDTOid> converter)
        {
            _measures = repository;
            _converter = converter;
        }

        [HttpGet]
        public IEnumerable<MeasureDTOid> Get()
        {
            return _measures.ReadAll().Select(_converter.AsDTOid);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (_measures.Read(id) == null)
                return NotFound();
            return Ok(_converter.AsDTO(_measures.Read(id)));
        }
        
        [HttpPost]
        public IActionResult Post(MeasureDTO measure)
        {
            if (measure == null)
            {
                return BadRequest();
            }
            _measures.Add(_converter.FromDTO(measure));
            return Ok(measure);
        }

        /// <response code="204">Item updated</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, MeasureDTO measure)
        {
            if (measure == null)
            {
                return BadRequest();
            }
            if (_measures.Read(id) == null)
            {
                return NotFound();
            }
            _measures.Edit(id, _converter.FromDTO(measure));
            return Ok(measure);
        }

        /// <response code="204">Item deleted</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_measures.Read(id) == null)
            {
                return NotFound();
            }
            _measures.Delete(id);
            return NoContent();
        }
    }
}
