using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiPubs.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly pubsContext context;
        public PublisherController(pubsContext context)
        {
            this.context = context;
        }
        //GET: api/publisher
        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> Get()
        {
            return context.Publishers.ToList();

        }
        //GET api/publisher/5
        [HttpGet("{id}")]
        public ActionResult<Publisher> GetById(string id)
        {
            Publisher publisher = (from a in context.Publishers
                                   where a.PubId == id
                                     select a).SingleOrDefault();
            return publisher;
        }
        //PUT api/publisher/2
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }
            context.Entry(publisher).State = EntityState.Modified; //lo marcamos como objeto modificado
            context.SaveChanges(); //hacemos la modificación
            return NoContent();
        }
        //POST api/publisher
        [HttpPost]
        public ActionResult Post(Publisher publisher)
        {
            if (!ModelState.IsValid) //si falló la validacion, entonces..
            {
                return BadRequest(ModelState);
            }
            context.Publishers.Add(publisher); //si está todo ok, mandamos a la base.
            context.SaveChanges();
            return Ok(); //ES CODIGO 200

        }
        [HttpDelete("{id}")]
        public ActionResult<Publisher> Delete(string id)
        {//la habitacion eliminada se lo mandamos al cliente.
            var publisher = (from a in context.Publishers
                              where a.PubId == id
                              select a).SingleOrDefault();
            //ahí buscamos la habitacion por id.

            if (publisher == null)
            {
                return NotFound();
            }
            context.Publishers.Remove(publisher);
            context.SaveChanges();
            return publisher;

        }
    }
}
