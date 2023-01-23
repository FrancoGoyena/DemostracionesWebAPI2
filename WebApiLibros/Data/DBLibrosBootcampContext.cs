using Microsoft.EntityFrameworkCore;
using WebApiLibros.Models;
namespace WebApiLibros.Data
{
    public class DBLibrosBootcampContext:DbContext
    {
        //contructor va siempre
        public DBLibrosBootcampContext(DbContextOptions<DBLibrosBootcampContext> options) : base(options) { }

        //Propiedades
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
