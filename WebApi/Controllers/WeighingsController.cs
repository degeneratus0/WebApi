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
    public class WeighingsController : ControllerBase
    {
        private readonly IRepository<Weighing> Weighings;
        private readonly IConverter<Weighing, WeighingDTO, WeighingDTOid> Converter;

        public WeighingsController(IRepository<Weighing> weighings, IConverter<Weighing, WeighingDTO, WeighingDTOid> converter)
        {
            Weighings = weighings;
            Weighings.Set();
            Converter = converter;
        }

        [HttpGet]
        public IEnumerable<WeighingDTOid> Get()
        {
            return Weighings.ReadAll().Select(Converter.AsDTOid);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Weighing weighing = Weighings.Read(id);
            if (weighing == null)
                return NotFound();
            return Ok(Converter.AsDTO(weighing));
        }

        [HttpPost]
        public IActionResult Post(WeighingDTO weighing)
        {
            try
            {
                Weighings.Add(Converter.FromDTO(weighing));
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
            if (Weighings.Read(id) == null)
            {
                return NotFound();
            }
            try
            {
                Weighings.Edit(id, Converter.FromDTO(weighing));
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
            if (Weighings.Read(id) == null)
            {
                return NotFound();
            }
            Weighings.Delete(id);
            return NoContent();
        }
    }
}
