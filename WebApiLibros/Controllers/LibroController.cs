using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic; //AGREGADO
using System.Linq;
using WebApiLibros.Data; //AGREGADO 
using WebApiLibros.Models; //Agregamos using

namespace WebApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        //                   ******* Inyeccion de dependencia --- > Inicia **************


        //Propiedad
        private readonly DbLibrosBootcampContext context;
        //Constructor
        public LibroController(DbLibrosBootcampContext context)
        {
            this.context = context;

        }
        //GET : api/Libro
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> GetLibro()
        {
            return context.Libros.ToList();
        }

        // GET api/autor/5 TRAER LIBRO POR ID 
        [HttpGet("{id}")]
        public ActionResult<Libro> GetbyIdLibro(int id)
        {
            Libro libro = (from a in context.Libros
                           where a.Id == id
                           select a).SingleOrDefault();
            return libro;
        }

        [HttpPost] // INSERTAR LIBROS --- RETORNAR OK 
        public ActionResult PostLibro(Libro libro)
        {
            if (!ModelState.IsValid) // hace una validación. Si no puede validar hace lo que sigue.
            {
                return BadRequest(libro);
            }
            context.Libros.Add(libro);
            context.SaveChanges();
            return Ok(); //devuelve status code 200 "caso de éxito"
        }


        [HttpPut("{id}")] // MODIFICAR LIBRO 
        public ActionResult PutLibro(int id, string titulo, [FromBody] Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }
            context.Entry(libro).State = EntityState.Modified; // o sino 
            context.SaveChanges();
            return Ok();
        }


        // DELETE 

        [HttpDelete("{id}")]
        public ActionResult<Libro> DeleteLibro(int id)
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

        [HttpGet("{IdAutor}")] // SELECT POR AUTOR ID 
        public ActionResult<Libro> GetbyAutor(int id)
        {
            Libro libro = (from l in context.Libros
                           where l.AutorId == id
                           select l).SingleOrDefault();
            return libro;
        }


    }
}
