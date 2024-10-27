using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.DTOs
{
    public class AutorDTOConLibros : AutorDTO
    {
        public List<LibroDTO> libros { get; set; }
        
    }
}