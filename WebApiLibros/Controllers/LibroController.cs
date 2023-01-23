using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiLibros.Data;
using WebApiLibros.Models;

namespace WebApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly DBLibrosBootcampContext context;
        public LibroController(DBLibrosBootcampContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.ToList();
        }

        [HttpGet("listado/{id}")]
        public ActionResult<IEnumerable<Libro>> GetByIdTodos(int id)
        {
            List<Libro> libro = (from a in context.Libros
                           where a.Id == id
                           select a).ToList();
            return libro;
        }

        [HttpGet("{id}")]
        public ActionResult<Libro> GetById(int id)
        {
            Libro libro = (from a in context.Libros
                           where a.AutorId == id
                           select a).SingleOrDefault();
            return libro;
        }

        [HttpPost]
        public ActionResult Post(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Libros.Add(libro);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }
            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = (from a in context.Libros
                         where a.Id == id
                         select a).SingleOrDefault();
            if (libro == null)
            {
                return NotFound();
            }
            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }

    }
}
