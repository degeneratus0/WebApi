using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataModelController : ControllerBase
    {
        private static List<string> context = new List<string>();

        /// <response code="201">Items set</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        [Route("Set")]
        public ActionResult Set([FromBody]List<string> newContext)
        {
            context = newContext;
            return Created(Url.Action(), context);
        }

        /// <response code="200">All items returned</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public List<string> Get()
        {
            return context;
        }

        /// <response code="200">Item returned</response>
        /// <response code="400">Bad request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0 || id >= context.Count)
            {
                return BadRequest();
            }
            return context[id];
        }

        /// <response code="201">Item created</response>
        /// <response code="400">Bad request</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<string> Post([FromBody]string item)
        {
            if (item == null || item == string.Empty)
            {
                return BadRequest();
            }
            context.Add(item);
            return Created("/api/DataModel", item);
        }

        /// <response code="204">Item updated</response>
        /// <response code="400">Bad request</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string item)
        {
            if (id < 0 || id >= context.Count)
            {
                return BadRequest();
            }
            context[id] = item;
            return NoContent();
        }

        /// <response code="204">Item updated</response>
        /// <response code="400">Bad request</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0 || id >= context.Count)
            {
                return BadRequest();
            }
            context.RemoveAt(id);
            return NoContent();
        }
    }
}
