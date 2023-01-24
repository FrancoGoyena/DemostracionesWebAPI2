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
    public class StoreController : ControllerBase
    {
        private readonly pubsContext context;
        public StoreController(pubsContext context)
        {
            this.context = context;
        }
        //GET: api/store
        [HttpGet]
        public ActionResult<IEnumerable<Store>> Get()
        {
            return context.Stores.ToList();

        }
        
        //PUT api/store/2
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Store store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }
            context.Entry(store).State = EntityState.Modified; //lo marcamos como objeto modificado
            context.SaveChanges(); //hacemos la modificación
            return NoContent();
        }
        //POST api/store
        [HttpPost]
        public ActionResult Post(Store store)
        {
            if (!ModelState.IsValid) //si falló la validacion, entonces..
            {
                return BadRequest(ModelState);
            }
            context.Stores.Add(store); //si está todo ok, mandamos a la base.
            context.SaveChanges();
            return Ok(); //ES CODIGO 200

        }
        [HttpDelete("{id}")]
        public ActionResult<Store> Delete(string id)
        {//el store eliminado se lo mandamos al cliente.
            var store = (from a in context.Stores
                             where a.StorId == id
                             select a).SingleOrDefault();
            //ahí buscamos el store por id.

            if (store == null)
            {
                return NotFound();
            }
            context.Stores.Remove(store);
            context.SaveChanges();
            return store;

        }

        //GET api/store/5
        [HttpGet("{id}")]
        public ActionResult<Store> GetById(string id)
        {
            Store store = (from a in context.Stores
                           where a.StorId == id
                           select a).SingleOrDefault();
            return store;
        }

        //GET api/store/nombre/name
        [HttpGet("nombre/{name}")]
        public ActionResult<Store> GetByName(string name)
        {
            Store store = (from a in context.Stores
                           where a.StorName == name
                           select a).SingleOrDefault();
            return store;
        }

        //GET api/store/zip/zip
        [HttpGet("zip/{zip}")]
        public ActionResult<Store> GetByZip(string zip)
        {
            Store store = (from a in context.Stores
                           where a.Zip == zip
                           select a).SingleOrDefault();
            return store;
        }

        //GET api/store/listado/citystate
        [HttpGet("listado/{city}/{state}")]
        public ActionResult<IEnumerable<Store>> GetByCityState(string city, string state)
        {
            List<Store> store = (from a in context.Stores
                                 where a.City == city && a.State == state
                                 select a).ToList();
            return store;
        }
    }
}
