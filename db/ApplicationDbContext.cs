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

        public DbSet<Autor> autores { get; set; }
        public DbSet<Libro> libros{ get; set; }
        public DbSet<Comentario> comentarios{ get; set; }
    }
}