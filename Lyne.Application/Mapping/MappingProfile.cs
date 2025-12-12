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
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.OrderIds, opt => opt.MapFrom(src => src.Orders.Select(o => o.Id)));
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore());
        CreateMap<UserDto, User>();
        // RegisterUserDto → User
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<UserDto, User>()
            .ForMember(d => d.PasswordHash, o => o.Ignore())
            .ForMember(d => d.Orders,       o => o.Ignore())
            .ForMember(d => d.Id,           o => o.Ignore())
            .ForMember(d => d.DateOfBirth,  o => o.MapFrom(s => DateOnly.Parse(s.DateOfBirth)));

        // Order → OrderDto
        // CreateMap<Order, OrderDto>()
        //     .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src => src.OrderProducts.Select(p => p.OrderId)));
        CreateMap<Order, OrderDto>()
            .ForMember(d => d.ProductIds, o => o.MapFrom(s => s.OrderProducts.Select(op => op.ProductId)))
            .ForMember(d => d.ShippingAddress, o => o.MapFrom(s => s.ShippingAddress));
        
        CreateMap<OrderDto, Order>()
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.ShippingAddress, o => o.Ignore())
            .ForMember(d => d.OrderProducts, o => o.Ignore());
        
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.OrderProducts, opt => opt.Ignore());
        CreateMap<OrderDto, Order>()
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.ShippingAddress, o => o.Ignore())
            .ForMember(d => d.OrderProducts, o => o.Ignore());
        // Category → CategoryDto
        CreateMap<Category, CategoryDto>()
            .ForMember(d => d.ProductIds,
                o => o.MapFrom(s => s.Products.Select(p => p.Id)));
        CreateMap<CategoryDto, Category>()
            .ForMember(d => d.Products, o => o.Ignore()); 
        
        // Address ↔ AddressDto
        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
        
        // Product ↔ ProductDto
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        
        CreateMap<Product, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}