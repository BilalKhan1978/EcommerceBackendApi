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

            CreateMap<Product, GetProductsRequestDto>();

            CreateMap<AddUserRequestDto, User>();

            CreateMap<GetStoreRequestDto, Store>();

            CreateMap<AddStoreRequestDto, Store>();
                      
            CreateMap<Store, GetStoreRequestDto>();

        }
      
    }
}
