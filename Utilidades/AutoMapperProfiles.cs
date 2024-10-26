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
            
            CreateMap<Libro,LibroDTO>()
            .ForMember(libroDTO=>libroDTO.autores, opciones=>opciones.MapFrom(MapLibrosDTOAutores))
            .ReverseMap();

           
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

        private List<AutorDTO> MapLibrosDTOAutores(Libro libro,LibroDTO libroDTO){
            var resultado = new List<AutorDTO>();
            if (libro.autoresLibros == null) return resultado;

            foreach (var autoresLibro in libro.autoresLibros )
            {
                resultado.Add(
                    new AutorDTO(){
                        id=autoresLibro.AutorId,
                        nombres=autoresLibro.autor.nombres
                    }
                );
            }
            return resultado;
        }
    }
}