using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TQ.Models;

namespace TQ.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private TestAppDataBaseContext _ctx;

        public ValuesController(TestAppDataBaseContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/values
        [HttpGet]
        public List<Buyer> Get()
        {
            return _ctx.Buyers.ToList();
        }

        [HttpGet("{id}")]
        public Buyer Get(int id)
        {
            return _ctx.Buyers.Find(id);
        }
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Buyer buyer)
        {
            if (buyer == null)
            {
                return BadRequest();
            }
            _ctx.Buyers.Add(buyer);
            _ctx.SaveChanges();
            return Ok(buyer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Buyer b)
        {
            
            Buyer buyer = _ctx.Buyers.Find(id);
            if (buyer == null)
            {
                return BadRequest();
            }
            buyer = b;

            _ctx.SaveChanges();
            return Ok(buyer);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Buyer buyer = _ctx.Buyers.Find(id);
            if (buyer == null)
            {
                return BadRequest();
            }
            _ctx.Buyers.Remove(buyer);

            _ctx.SaveChanges();
            return Ok();
        }
    }
}
