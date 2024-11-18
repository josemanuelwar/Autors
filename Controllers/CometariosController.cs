using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResFull.db;
using ApiResFull.DTOs;
using ApiResFull.Entidades;
using ApiResFull.Migrations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiResFull.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class CometariosController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public IMapper Mapper { get; }

        public CometariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CometariosDTO>>> Get(int libroId){
            
            var existeLibro = await this.Context.libros.AnyAsync(LibroDB => LibroDB.id == libroId);
            if (!existeLibro){
                return NotFound();
            }

            var cometarios = await this.Context.comentarios
            .Where(cometarioDb => cometarioDb.libroId == libroId).ToListAsync();

            return this.Mapper.Map<List<CometariosDTO>>(cometarios);
        }

        [HttpGet("{id:int}", Name = "ComentarioLibro")]
        public async Task<ActionResult<CometariosDTO>> GetId(int id){
            var comentario= await this.Context.comentarios.FirstOrDefaultAsync(x=>x.id == id);

            if (comentario == null){
                return NotFound();
            }   

             return this.Mapper.Map<CometariosDTO>(comentario);
        }

        [HttpPost]
        public async Task<ActionResult>Post(int libroId, CometarioCreationDTO cometarioCreationDTO){

            var existeLibro = await this.Context.libros.AnyAsync(LibroDB => LibroDB.id == libroId);
            
            if (!existeLibro){
                return NotFound();
            }

            var comentario= this.Mapper.Map<Comentario>(cometarioCreationDTO);
            comentario.libroId = libroId;
            this.Context.Add(comentario);
            await this.Context.SaveChangesAsync();

            var cometariosDTO = this.Mapper.Map<CometariosDTO>(comentario);

            return CreatedAtRoute("ComentarioLibro",  new {id = comentario.id, libroId=libroId}, cometariosDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int libroId,int id,CometarioCreationDTO cometarioCreationDTO){
            var exiteLibro = await this.Context.libros.AnyAsync(x => x.id == libroId);

            if(!exiteLibro) return NotFound();

            var existeComentario = await this.Context.comentarios.AnyAsync(x => x.id == id);
            
            if (!existeComentario) return NotFound();

            var comeentario= this.Mapper.Map<Comentario>(cometarioCreationDTO);

            comeentario.id=id;
            comeentario.libroId=libroId;
            this.Context.Update(comeentario);
            await this.Context.SaveChangesAsync();
            return NoContent();

        }
    }
}