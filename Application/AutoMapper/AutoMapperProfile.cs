using AutoMapper;
using Data.Entities;
using Models.OrderDetails;
using Models.Orders;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderVm, Order>();
            CreateMap<OrderDetailsVm, OrderDetail>();
        }
    }
}