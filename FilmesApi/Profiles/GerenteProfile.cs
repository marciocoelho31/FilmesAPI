using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using System.Linq;

namespace FilmesApi.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, ReadGerenteDto>()
                .ForMember(g => g.Cinemas, opts => opts
                    .MapFrom(g => g.Cinemas.Select
                    (c => new { c.Id, c.Nome, c.Endereco, c.EnderecoId })));
        }
    }
}
