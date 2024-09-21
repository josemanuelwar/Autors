using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResFull.db;
using ApiResFull.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiResFull.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController:ControllerBase{

        private readonly ApplicationDbContext context;
        public AutoresController(ApplicationDbContext context){
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get(){
            
            return await this.context.Autores.Include(x => x.libros).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor){
            this.context.Add(autor);
            await this.context.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id){
            if(autor.id != id){
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            this.context.Update(autor);
            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id){
            var existe = await this.context.Autores.AnyAsync(x=>x.id == id);
            if(!existe){
                return NotFound();
            }
            this.context.Remove(new Autor(){ id = id });
            await this.context.SaveChangesAsync();
            return Ok();
        }
        
    }
} 