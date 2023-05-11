using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/measures")]
    [ApiController]
    public class MeasuresController : ControllerBase
    {
        IData<Measure> Measures;
        IConverter<Measure, MeasureDTO, MeasureDTOid> DTO;

        public MeasuresController(IData<Measure> measures, IConverter<Measure, MeasureDTO, MeasureDTOid> dto)
        {
            Measures = measures;
            Measures.Set();
            DTO = dto;
        }

        [HttpGet]
        public IEnumerable<MeasureDTOid> Get()
        {
            return Measures.ReadAll().Select(DTO.AsDTOid);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (Measures.Read(id) == null)
                return NotFound();
            return Ok(DTO.AsDTO(Measures.Read(id)));
        }
        
        [HttpPost]
        public IActionResult Post(MeasureDTO measure)
        {
            if (measure == null)
            {
                return BadRequest();
            }
            Measures.Add(DTO.FromDTO(measure));
            return Ok(measure);
        }

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
            Measures.Edit(id, DTO.FromDTO(measure));
            return Ok(measure);
        }

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
