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
    public class AutorController : ControllerBase
    {
        //                   ******* Inyeccion de dependencia --- > Inicia **************


        //Propiedad
        private readonly DbLibrosBootcampContext context;
        //Constructor
        public AutorController(DbLibrosBootcampContext context)
        {
            this.context = context;

        }
        //GET : api/alumno
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get() 
        { 
            return context.Autores.ToList();
        }

        // GET api/autor/5
        [HttpGet("{id}")]
        public ActionResult<Autor> GetbyId(int id)
        {
            Autor autor = (from a in context.Autores
                           where a.IdAutor == id
                           select a).SingleOrDefault();
            return autor;
        }


        // POST api/autor --- INSERTAR 
        [HttpPost]
        public ActionResult Post(Autor autor)
        {
            if (!ModelState.IsValid) // hace una validación. Si no puede validar hace lo que sigue.
            {
                return BadRequest(autor);
            }
            context.Autores.Add(autor);
            context.SaveChanges();
            return Ok(); //devuelve status code 200 "caso de éxito"
        }

        //UPDATE --- MODIFICAR
        //PUT api/autor/2
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Autor autor)
        {
            if (id!=autor.IdAutor)
            {
                return BadRequest();
            }
            context.Entry(autor).State= EntityState.Modified; // o sino 
            context.SaveChanges();
            return Ok();
        }

        // DELETE api/autor/1

        [HttpDelete("{id}")] // las {} indican que es una cadena, sino sin ellas sería un parámetro
        public ActionResult<Autor> Delete(int id)
        {
            var autor =(from a in context.Autores
                        where a.IdAutor == id
                        select a).SingleOrDefault();
            if (autor == null)
            {

                return NotFound();

            }
            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;

        }

        //GET: api/autor
        [HttpGet("{listado/edad}")]
        public ActionResult<IEnumerable<Autor>> Get(int edad)
        {
            List<Autor> autores = (from a in context.Autores
                                   where a.Edad == edad
                                   select a).ToList();
            return autores;
        }


    }
}
