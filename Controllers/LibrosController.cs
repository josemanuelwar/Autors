using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResFull.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiResFull.Entidades;

namespace ApiResFull.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController :ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context){

            this.context = context;

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLibros(int id){
            var libro= await this.context.libros.Include(x => x.autor).FirstOrDefaultAsync(x=> x.id==id);
            if(libro == null){
                return NotFound("No se encontro el libro");
            }
            return Ok(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro){
            var existeAutor = await this.context.Autores.AnyAsync(x=> x.id==libro.autorId);
            if(!existeAutor){
                return BadRequest($"No existe el autor de Id: {libro.autorId}");
            }

            this.context.Add(libro);
            await this.context.SaveChangesAsync();
            return Ok(libro);
        }
    }
}