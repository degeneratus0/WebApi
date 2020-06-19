using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class WeighingsController : ControllerBase
    {
        WeighingsContext db;
        public WeighingsController(WeighingsContext context)
        {
            db = context;
            if (!db.Weighings.Any())
            {
                db.Weighings.Add(new Weighing { Item = "Сок", Weight = 200, Measure = "г", TareType = "Коробка" });
                db.Weighings.Add(new Weighing { Item = "Лимонад", Weight = 1000, Measure = "г", TareType = "Бутылка" });
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Возвращает все записи
        /// </summary>
        // GET: api/<WeighingsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weighing>>> Get()
        {
            return await db.Weighings.ToListAsync();
        }
        /// <summary>
        /// Возвращает определённую запись
        /// </summary>
        // GET api/<WeighingsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weighing>> Get(int id)
        {
            Weighing weighing = await db.Weighings.FirstOrDefaultAsync(x => x.IDWeighing == id);
            if (weighing == null)
                return NotFound();
            return new ObjectResult(weighing);
        }
        /// <summary>
        /// Добавляет запись
        /// </summary>
        // POST api/<WeighingsController>
        [HttpPost]
        public async Task<ActionResult<Weighing>> Post(Weighing weighing)
        {
            if (weighing == null)
            {
                return BadRequest();
            }
            weighing.IDWeighing = 0;
            db.Weighings.Add(weighing);
            await db.SaveChangesAsync();
            return Ok(weighing);
        }
        /// <summary>
        /// Изменяет определённую запись
        /// </summary>
        // PUT api/<WeighingsController>
        [HttpPut]
        public async Task<ActionResult<Weighing>> Put(Weighing weighing)
        {
            if (weighing == null)
            {
                return BadRequest();
            }
            if (!db.Weighings.Any(x => x.IDWeighing == weighing.IDWeighing))
            {
                return NotFound();
            }
            db.Update(weighing);
            await db.SaveChangesAsync();
            return Ok(weighing);
        }
        /// <summary>
        /// Удаляет определённую запись
        /// </summary>
        /// <response code="204">Запись удалена</response>  
        // DELETE api/<WeighingsController>/5
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
    }
}
