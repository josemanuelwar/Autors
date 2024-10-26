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
            CreateMap<LibroCreationDTO,Libro>()
            .ForMember(libro=>libro.autoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));
            CreateMap<Libro,LibroDTO>().ReverseMap();
            CreateMap<CometarioCreationDTO,Comentario>();
            CreateMap<Comentario,CometariosDTO>().ReverseMap();
        }

        private List<AutorLibro> MapAutoresLibros(LibroCreationDTO libroCreationDTO, Libro libro){
            var resultado = new List<AutorLibro>();

            if (libroCreationDTO.AutoresIds == null) return resultado;

            foreach (var autorId in libroCreationDTO.AutoresIds)
            {
                resultado.Add(new AutorLibro{ AutorId = autorId});
            }


            return resultado ;

        }
    }
}