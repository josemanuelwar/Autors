using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResFull.DTOs;
using ApiResFull.Entidades;
using AutoMapper;

namespace ApiResFull.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(){
            CreateMap<AutorCreationDTO,Autor>();
            CreateMap<Autor,AutorDTO>().ReverseMap();
            CreateMap<LibroCreationDTO,Libro>();
            CreateMap<Libro,LibroDTO>().ReverseMap();
            CreateMap<CometarioCreationDTO,Comentario>();
            CreateMap<Comentario,CometariosDTO>().ReverseMap();
        }
    }
}