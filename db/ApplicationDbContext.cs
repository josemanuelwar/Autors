using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResFull.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiResFull.db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext( DbContextOptions options):base(options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AutorLibro>().HasKey(x=> new {x.LibroId,x.AutorId} );
        }

        public DbSet<Autor> autores { get; set; }
        public DbSet<Libro> libros{ get; set; }
        public DbSet<Comentario> comentarios{ get; set; }

        public DbSet<AutorLibro> autorLibro { get; set; }
    }
}