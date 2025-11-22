using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.Mappings;

public class BookStoreMappingProfile : Profile
{
    public BookStoreMappingProfile()
    {
        CreateMap<Livro, LivroDTO>()
            .ForMember(dest => dest.AutoresIds, opt => opt.MapFrom(src => src.LivroAutores.Select(la => la.Autor_CodAu)))
            .ForMember(dest => dest.AssuntosIds, opt => opt.MapFrom(src => src.LivroAssuntos.Select(la => la.Assunto_CodAs)))
            .ForMember(dest => dest.Valores, opt => opt.MapFrom(src => src.LivroValores));

        CreateMap<LivroDTO, Livro>()
            .ForMember(dest => dest.LivroAutores, opt => opt.Ignore())
            .ForMember(dest => dest.LivroAssuntos, opt => opt.Ignore())
            .ForMember(dest => dest.LivroValores, opt => opt.Ignore());

        CreateMap<CreateLivroDTO, Livro>()
            .ForMember(dest => dest.CodL, opt => opt.Ignore())
            .ForMember(dest => dest.LivroAutores, opt => opt.Ignore())
            .ForMember(dest => dest.LivroAssuntos, opt => opt.Ignore())
            .ForMember(dest => dest.LivroValores, opt => opt.Ignore());

        CreateMap<Autor, AutorDTO>().ReverseMap();
        CreateMap<CreateAutorDTO, Autor>()
            .ForMember(dest => dest.CodAu, opt => opt.Ignore())
            .ForMember(dest => dest.LivroAutores, opt => opt.Ignore());

        CreateMap<Assunto, AssuntoDTO>().ReverseMap();
        CreateMap<CreateAssuntoDTO, Assunto>()
            .ForMember(dest => dest.CodAs, opt => opt.Ignore())
            .ForMember(dest => dest.LivroAssuntos, opt => opt.Ignore());
        CreateMap<TipoVenda, TipoVendaDTO>().ReverseMap();
        
        CreateMap<LivroValor, LivroValorDTO>()
            .ForMember(dest => dest.TipoVendaId, opt => opt.MapFrom(src => src.TipoVenda_CodTv));
        
        CreateMap<LivroValorDTO, LivroValor>()
            .ForMember(dest => dest.TipoVenda_CodTv, opt => opt.MapFrom(src => src.TipoVendaId));
    }
}