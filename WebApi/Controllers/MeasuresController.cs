using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasuresController : ControllerBase
    {
        private readonly IRepository<Measure> Measures;
        private readonly IConverter<Measure, MeasureDTO, MeasureDTOid> Converter;

        public MeasuresController(IRepository<Measure> measures, IConverter<Measure, MeasureDTO, MeasureDTOid> converter)
        {
            Measures = measures;
            Converter = converter;

            Measures.Set();
        }

        [HttpGet]
        public IEnumerable<MeasureDTOid> Get()
        {
            return Measures.ReadAll().Select(Converter.AsDTOid);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (Measures.Read(id) == null)
                return NotFound();
            return Ok(Converter.AsDTO(Measures.Read(id)));
        }
        
        [HttpPost]
        public IActionResult Post(MeasureDTO measure)
        {
            if (measure == null)
            {
                return BadRequest();
            }
            Measures.Add(Converter.FromDTO(measure));
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
            if (Measures.Read(id) == null)
            {
                return NotFound();
            }
            Measures.Edit(id, Converter.FromDTO(measure));
            return Ok(measure);
        }

        /// <response code="204">Item deleted</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (Measures.Read(id) == null)
            {
                return NotFound();
            }
            Measures.Delete(id);
            return NoContent();
        }
    }
}
