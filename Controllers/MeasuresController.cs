using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/measures")]
    [ApiController]
    public class MeasuresController : ControllerBase
    {
        IWeighings<Measure, MeasureDTO, MeasureDTOid> Measures;
        public MeasuresController(IWeighings<Measure, MeasureDTO, MeasureDTOid> measures)
        {
            Measures = measures;
            Measures.Set();
        }

        [HttpGet]
        public IEnumerable<MeasureDTOid> Get()
        {
            return Measures.ReadAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (Measures.Read(id) == null)
                return NotFound();
            return Ok(Measures.Read(id));
        }
        
        [HttpPost]
        public IActionResult Post(MeasureDTO measure)
        {
            if (measure == null)
            {
                return BadRequest();
            }
            Measures.Add(measure);
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
            Measures.Edit(id, measure);
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
