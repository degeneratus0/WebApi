using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/weighings")]
    [ApiController]
    public class WeighingsController : ControllerBase
    {
        
        IData<Weighing> Weighings;
        IConverter<Weighing, WeighingDTO, WeighingDTOid> DTO;
        public WeighingsController(IData<Weighing> weighings, IConverter<Weighing, WeighingDTO, WeighingDTOid> dto)
        {
            Weighings = weighings;
            Weighings.Set();
            DTO = dto;
        }

        [HttpGet]
        public IEnumerable<WeighingDTOid> Get()
        {
            return Weighings.ReadAll().Select(DTO.AsDTOid);
        }
        
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (Weighings.Read(id) == null)
                return NotFound();
            return Ok(DTO.AsDTO(Weighings.Read(id)));
        }

        [HttpPost]
        public IActionResult Post(WeighingDTO weighing)
        {
            try
            {
                Weighings.Add(DTO.FromDTO(weighing));
                return Ok(weighing);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(int id, WeighingDTO weighing)
        {
            if (Weighings.Read(id) == null)
            {
                return NotFound();
            }
            try
            {
                Weighings.Edit(id, DTO.FromDTO(weighing));
                return Ok(weighing);
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <response code="204">Запись удалена</response>  
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
