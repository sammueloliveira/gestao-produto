using AutoMapper;
using CatalogoProduto.APIs.Models.DTOs;
using CatalogoProduto.Domain.Entities;

namespace CatalogoProduto.APIs.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Produto, UpdateProdutoDTO>().ReverseMap();
        }
    }
}
