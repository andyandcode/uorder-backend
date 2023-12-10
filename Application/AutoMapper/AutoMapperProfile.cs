using AutoMapper;
using Data.Entities;
using Models.Accounts;
using Models.DiscountCodes;
using Models.OrderDetails;
using Models.Orders;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderVm, Order>();
            CreateMap<AccountVm, Account>();
            CreateMap<OrderDetailsVm, OrderDetail>();
            CreateMap<DiscountCodeVm, DiscountCode>();
        }
    }
}