using AutoMapper;
using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;

namespace EcommerceBackendApi.Mapper
{
    public class AutoMapperProfiles : Profile
    {
      public AutoMapperProfiles()
       {
            CreateMap<AddProductRequestDto, Product>();
            CreateMap<UpdateProductRequestDto, Product>();
            CreateMap<Product, GetProductsRequestDto>();

            CreateMap<AddUserRequestDto, User>();
            CreateMap<User, GetUserRequestDto>();

            CreateMap<AddStoreRequestDto, Store>();
            CreateMap<Store, GetStoreRequestDto>();
      }
      
    }
}
