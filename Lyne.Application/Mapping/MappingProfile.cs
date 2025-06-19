using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;

namespace Lyne.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User → UserDto
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        
        // RegisterUserDto → User
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        // Order → OrderDto
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src => src.Products.Select(p => p.Id)));
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        // Category → CategoryDto
        CreateMap<Category, CategoryDto>();

        // Address → AddressDto
        CreateMap<Address, AddressDto>();
    }
}