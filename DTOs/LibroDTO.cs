using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.DTOs
{
    public class LibroDTO
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public List<AutorDTO> autores { get; set; }
        public List<CometariosDTO> comentarios{ get; set; }
    }
}