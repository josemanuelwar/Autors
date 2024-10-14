using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResFull.db;
using ApiResFull.DTOs;
using ApiResFull.Entidades;
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

        [HttpPost]
        public async Task<ActionResult>Post(int libroId, CometarioCreationDTO cometarioCreationDTO){

            var existeLibro = await this.Context.libros.AnyAsync(LibroDB => LibroDB.id == libroId);
            if (!existeLibro){
                return NotFound();
            }

            var cometario= this.Mapper.Map<Comentario>(cometarioCreationDTO);
            cometario.libroId = libroId;
            this.Context.Add(cometario);
            await this.Context.SaveChangesAsync();

            return Ok(cometario);
        }
    }
}