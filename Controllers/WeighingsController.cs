using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.FileProviders;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
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
                db.Weighings.Add(new Weighing { Item = "Кирпич", Weight = 2, Measure = "кг", TareType = "Без тары"});
                db.Weighings.Add(new Weighing { Item = "Молоко", Weight = 500, Measure = "г", TareType = "Стакан" });
                db.SaveChanges();
            }
        }
        // GET: api/<WeighingsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weighing>>> Get()
        {
            return await db.Weighings.ToListAsync();
        }

        // GET api/<WeighingsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weighing>> Get(int id)
        {
            Weighing weighing = await db.Weighings.FirstOrDefaultAsync(x => x.IDWeighing == id);
            if (weighing == null)
                return NotFound();
            return new ObjectResult(weighing);
        }

        // POST api/<WeighingsController>
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

        // PUT api/<WeighingsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Weighing>> Put(Weighing weighing)
        {
            if (weighing == null)
            {
                return BadRequest();
            }
            if(!db.Weighings.Any(x => x.IDWeighing == weighing.IDWeighing))
            {
                return NotFound();
            }
            db.Update(weighing);
            await db.SaveChangesAsync();
            return Ok(weighing);
        }

        // DELETE api/<WeighingsController>/5
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
            return Ok(weighing);
        }
    }
}
