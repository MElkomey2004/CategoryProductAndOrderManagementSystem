using AutoMapper;
using CategoryProductAndOrderManagementSystem.DTOs;
using CategoryProductAndOrderManagementSystem.Modles;

namespace CategoryProductAndOrderManagementSystem.Automapper
{
	public class MapProfile :Profile
	{
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<OrderDto, Order>()
			  .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

			CreateMap<OrderItemDto, OrderItem>()
				.ForMember(dest => dest.OrderId, opt => opt.Ignore())
				.ForMember(dest => dest.Order, opt => opt.Ignore());

		}
    }
}
