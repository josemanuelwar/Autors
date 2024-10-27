using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.DTOs
{
    public class LibroDTOConAutores :LibroDTO
    {
         public List<AutorDTO> autores { get; set; }
    }
}