using System.Linq;
using AutoMapper;
using ProAgil.Domain;
using ProAgil.WebAPI.DTOs;

namespace ProAgil.WebAPI.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<Evento, EventoDto>()
          .ForMember(dest => dest.Palestrantes, opt =>
          {
            opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Palestrante).ToList());
          }).ReverseMap();      

      CreateMap<Palestrante, PalestranteDto>()
        .ForMember(dest => dest.Eventos, opt =>
        {
          opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Evento).ToList());
        }).ReverseMap();      
      CreateMap<Lote, LoteDto>().ReverseMap();
      CreateMap<LoteDto, Lote>().ReverseMap();
      CreateMap<RedeSocialDto, RedeSocial>().ReverseMap();
    }
  }
}
