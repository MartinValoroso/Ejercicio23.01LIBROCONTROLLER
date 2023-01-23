using Microsoft.EntityFrameworkCore; // Agregamos el USING
using WebApiLibros.Models;
using WebApiLibros.Data;




namespace WebApiLibros.Data

{
    public class DbLibrosBootcampContext : DbContext
    {
        //Agrego el constructor
        public DbLibrosBootcampContext(DbContextOptions<DbLibrosBootcampContext> options) : base(options) { } 

        // DBSET --- > Propiedades

        public DbSet<Autor>Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }


    }
}
