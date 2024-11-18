using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResFull.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiResFull.DTOs;
using AutoMapper;
using ApiResFull.Entidades;

namespace ApiResFull.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController :ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper maper;
        public LibrosController(ApplicationDbContext context, IMapper mapper){

            this.context = context;
            this.maper = mapper;

        }

       [HttpGet("{id:int}", Name ="ObtenerLibro")]
        public async Task<ActionResult<LibroDTOConAutores>> GetLibros(int id){
            var libro= await this.context.libros
            //.Include(com => com.comentarios)
            .Include(libroDB =>libroDB.autoresLibros)
            .ThenInclude(autoresLibroDB=>autoresLibroDB.autor)
            .FirstOrDefaultAsync(x=> x.id==id);

            if(libro == null){
                return NotFound("No se encontro el libro");
            }
            libro.autoresLibros=libro.autoresLibros.OrderBy(x => x.orden).ToList();

            var libros = this.maper.Map<LibroDTOConAutores>(libro);
            return libros;
        }

        [HttpGet("comentario/{id:int}")]

        public async Task<ActionResult<LibroDTOComentarios>> GetLibrosComentarios(int id){
            var libro= await this.context.libros.Include(libroDB =>libroDB.comentarios).FirstOrDefaultAsync(x=>x.id==id);

            if(libro == null) return NotFound("No se encontro un libro");

            var LibroComentario=this.maper.Map<LibroDTOComentarios>(libro);

            return LibroComentario;
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreationDTO libroCreationDTO){

            if(libroCreationDTO.AutoresIds == null){
                return BadRequest("No se puede crear un libro si autores");
            }

            var autoresId = await this.context.autores
                            .Where(x => libroCreationDTO.AutoresIds.Contains(x.id))
                            .Select(x=>x.id).ToListAsync();

            if(libroCreationDTO.AutoresIds.Count != autoresId.Count){
                return BadRequest("No existe uno de los autores enviados");
            }

            var libro = this.maper.Map<Libro>(libroCreationDTO);

            if(libro.autoresLibros != null){
                for(int i = 0; i < libro.autoresLibros.Count; i++){
                    libro.autoresLibros[i].orden=i;
                }
            }
            this.context.Add(libro);
            await this.context.SaveChangesAsync();

            var libtoDTO=this.maper.Map<LibroDTO>(libro);
            
            return CreatedAtRoute("ObtenerLibro", new {id=libro.id}, libtoDTO);
        }
    }
}