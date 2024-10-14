using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using ApiResFull.db;
using ApiResFull.DTOs;
using ApiResFull.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiResFull.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController:ControllerBase{

        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        public AutoresController(ApplicationDbContext context, IMapper mapper){
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> Get(){

            var autores =await this.context.autores.ToListAsync();
            
            return this.mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDTO>> GetById([FromRoute] int id){
            var autor=  await this.context.autores.FirstOrDefaultAsync(x => x.id == id);
            if(autor==null){
                return NotFound("Usuario no encontrado");
            }

            return this.mapper.Map<AutorDTO>(autor);
        }
        
        [HttpGet("{nombre}")]

        public async Task<ActionResult<List<AutorDTO>>> GetName(string nombre){

            var autore = await this.context.autores.Where(autorDB => autorDB.nombres.Contains(nombre)).ToListAsync();

              return this.mapper.Map<List<AutorDTO>>(autore);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreationDTO autorCreationDTO){
            var exiteAutor= await this.context.autores.AnyAsync(x=>x.nombres == autorCreationDTO.nombres);
            if(exiteAutor){
                return BadRequest($"Ya exite un autor con el nombre {autorCreationDTO.nombres}");
            }
            var autor = this.mapper.Map<Autor>(autorCreationDTO);
            this.context.Add(autor);
            await this.context.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(AutorDTO autorDTO, int id){
            if(autorDTO.id != id){
                return BadRequest("El id del autor no coincide con el id de la URL");
            }
            var autor = this.mapper.Map<Autor>(autorDTO);
            this.context.Update(autor);
            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id){
            var existe = await this.context.autores.AnyAsync(x=>x.id == id);
            if(!existe){
                return NotFound();
            }
            this.context.Remove(new Autor(){ id = id });
            await this.context.SaveChangesAsync();
            return Ok();
        }
        
    }
} 