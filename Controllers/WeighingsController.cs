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
        WeighingsContext db;
        public WeighingsController(WeighingsContext context)
        {
            db = context;

            if (!db.Weighings.Any())
            {
                db.Measures.Add(new Measure { MeasureName = "г" });
                db.Measures.Add(new Measure { MeasureName = "кг" });
                db.Weighings.Add(new Weighing { Item = "Сок", Weight = 200, idMeasure = 1, TareType = "Коробка" });
                db.Weighings.Add(new Weighing { Item = "Лимонад", Weight = 1, idMeasure = 2, TareType = "Бутылка" });
                db.SaveChanges();
            }
        }
        private static readonly Expression<Func<Weighing, WeighingDTO>> AsWeighingDTO =
            x => new WeighingDTO
            {
                Item = x.Item,
                Weight = x.Weight,
                Measure = x.Measure.MeasureName,
                TareType = x.TareType
            };

        [HttpGet]
        public IEnumerable<WeighingDTO> GetWeighings()
        {
            return db.Weighings.Include(x => x.Measure).Select(AsWeighingDTO);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeighing(int id)
        {
            WeighingDTO weighing = await db.Weighings.Include(x => x.Measure)
                .Where(x => x.IDWeighing == id)
                .Select(AsWeighingDTO)
                .FirstOrDefaultAsync();
            if (weighing == null)
                return NotFound();
            return Ok(weighing);
        }
        /*
        [HttpPost]
        public async Task<ActionResult<Weighing>> Post(Weighing weighing)
        {
            if (weighing == null)
            {
                return BadRequest();
            }
            db.Weighings.Add(weighing);
            await db.SaveChangesAsync();
            return Ok(weighing);
        }

        [HttpPut]
        public async Task<ActionResult<Weighing>> Put(int id, Weighing weighing)
        {
            if (weighing == null)
            {
                return BadRequest();
            }
            if (!db.Weighings.Any(x => x.IDWeighing == id))
            {
                return NotFound();
            }
            db.Weighings.Update(weighing);
            await db.SaveChangesAsync();
            return Ok(weighing);
        }

        /// <response code="204">Запись удалена</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Weighing>> Delete(int id)
        {
            Weighing weighing = db.Weighings.FirstOrDefault(x => x.IDWeighing == id);
            if (weighing == null)
            {
                return NotFound();
            }
            db.Weighings.Remove(weighing);
            await db.SaveChangesAsync();
            return NoContent();
        }
        */
    }
}
