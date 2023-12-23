using AutoMapper;
using Data.Entities;
using Models.Accounts;
using Models.DiscountCodes;
using Models.Dishes;
using Models.Menus;
using Models.OrderDetails;
using Models.Orders;
using Models.Tables;

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
            CreateMap<DishVm, Dish>();
            CreateMap<MenuVm, Menu>();
            CreateMap<TableVm, Table>();
        }
    }
}