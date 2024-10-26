using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.Entidades
{
    public class AutorLibro
    {
        public int LibroId { get; set; }
        public int  AutorId {get; set; }
        public int orden { get; set; }

        public Libro libro { get; set; }
        public Autor autor { get; set; }
    }
}