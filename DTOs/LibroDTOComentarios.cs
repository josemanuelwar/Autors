using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.DTOs
{
    public class LibroDTOComentarios : LibroDTO
    {
        public List<CometariosDTO> comentarios{ get; set; }
        
    }
}