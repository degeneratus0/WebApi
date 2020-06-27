using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/weighings")]
    [ApiController]
    public class WeighingsController : ControllerBase
    {
        
        IWeighings<Weighing, WeighingDTO, WeighingDTOid> Weighings;
        public WeighingsController(IWeighings<Weighing, WeighingDTO, WeighingDTOid> weighings)
        {
            Weighings = weighings;
            Weighings.Set();
        }

        [HttpGet]
        public IEnumerable<WeighingDTOid> Get()
        {
            return Weighings.ReadAll();
        }
        
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (Weighings.Read(id) == null)
                return NotFound();
            return Ok(Weighings.Read(id));
        }
        [HttpPost]
        public IActionResult Post(WeighingDTO weighing)
        {
            try
            {
                Weighings.Add(weighing);
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
            if (weighing == null)
            {
                return BadRequest();
            }
            if (Weighings.Read(id) == null)
            {
                return NotFound();
            }
            Weighings.Edit(id, weighing);
            return Ok(weighing);
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
