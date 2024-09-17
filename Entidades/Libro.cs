using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.Entidades
{
    public class Libro
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public int autorId { get; set; }
        public Autor autor{ get; set; }

    }
}