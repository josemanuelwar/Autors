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

       [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> GetLibros(int id){
            var libro= await this.context.libros
            .Include(com => com.comentarios)
            .FirstOrDefaultAsync(x=> x.id==id);

            if(libro == null){
                return NotFound("No se encontro el libro");
            }

            var libros = this.maper.Map<LibroDTO>(libro);
            return libros;
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
            this.context.Add(libro);
            await this.context.SaveChangesAsync();
            return Ok(libro);
        }
    }
}