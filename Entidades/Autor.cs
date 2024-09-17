using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.Entidades
{
    public class Autor
    {
        public int id { get; set; }

        public string nombres { get; set; }

        public List<Libro> libros { get; set; }
    }
}