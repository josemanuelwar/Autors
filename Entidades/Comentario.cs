using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.Entidades
{
    public class Comentario
    {
        public int id { get; set; }
        public string contenido { get; set; }
        public int libroId { get; set; }
        public Libro libro { get; set; }
    }
}