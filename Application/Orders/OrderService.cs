using Application.Dishes;
using Application.Payment;
using Application.SystemSettings;
using AutoMapper;
using Data.EF;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Models.Analystic;
using Models.OrderDetails;
using Models.Orders;

namespace Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly UOrderDbContext _context;
        private readonly IDishService _dishService;
        private readonly ISystemSettingService _systemSettingService;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(UOrderDbContext dbContext, IDishService dishService, IMapper mapper, ISystemSettingService systemSettingService, IPaymentService paymentService)
        {
            _context = dbContext;
            _dishService = dishService;
            _mapper = mapper;
            _systemSettingService = systemSettingService;
            _paymentService = paymentService;
        }

        public async Task<string> Create(OrderCreateRequest req)
        {
            var id = req.Id;
            var item = new Order
            {
                Id = id,
                Total = req.Total,
                Note = req.Note,
                TableId = req.TableId,
                OrderStatus = OrderStatus.Ordered,
                PaymentStatus = req.PaymentMethod == PaymentMethod.Cash ? PaymentStatus.Paid : PaymentStatus.Unpaid,
                CreatedAt = req.CreatedAt,
                OrderType = req.OrderType,
                Subtotal = req.Subtotal,
                Discount = req.Discount,
                PaymentMethod = req.PaymentMethod,
                MoneyChange = req.PaymentMethod == PaymentMethod.Cash ? req.MoneyChange : 0,
                MoneyReceive = req.PaymentMethod == PaymentMethod.Cash ? req.MoneyReceive : req.PaymentStatus == PaymentStatus.Unpaid ? 0 : req.Total,
                Staff = req.OrderType == OrderType.TakeAway ? req.Staff : "",
            };

            foreach (var child in req.OrderDetails)
            {
                var connect = new OrderDetail
                {
                    DishName = _dishService.GetById(child.DishId).Result.Name,
                    OrderId = id,
                    DishId = child.DishId,
                    Qty = child.Qty,
                    Amount = child.Amount,
                    UnitPrice = child.UnitPrice,
                    DishNote = child.DishNote,
                };
                _context.OrderDetails.Add(connect);
            }
            _context.Add(item);
            await _context.SaveChangesAsync();

            if (req.PaymentMethod == PaymentMethod.Momo)
            {
                return _paymentService.VnPayPayment(req, id);
            }
            return null;
        }

        public async Task<int> Update(OrderUpdateRequest req)
        {
            var item = new Order
            {
                Id = req.Id,
                Total = req.Total,
                Note = req.Note,
                TableId = req.TableId,
                OrderStatus = req.OrderStatus,
                PaymentStatus = req.PaymentStatus,
                CreatedAt = req.CreatedAt,
                OrderType = req.OrderType,
                MoneyChange = req.MoneyChange,
                MoneyReceive = req.MoneyReceive,
                Staff = req.Staff,
            };
            _context.Update(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateOrderStatus(string id, JsonPatchDocument<Order> patchDoc)
        {
            var stockItem = await GetById(id);
            var userDto = _mapper.Map<Order>(stockItem);
            patchDoc.ApplyTo(userDto);
            _context.Update(userDto);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Orders.FindAsync(id);
            if (item == null)
                return 0;

            _context.Orders.Remove(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<OrderVm>> GetAllOrder()
        {
            return await _context.Orders.Include(one => one.OrderDetails).Where(p => p.OrderType == OrderType.TakeAway).Select(p => new OrderVm()
            {
                Key = p.Id,
                Id = p.Id,
                Total = p.Total,
                Note = p.Note,
                TableId = p.TableId,
                TableName = p.Table.Name,
                OrderStatus = p.OrderStatus,
                OrderStatusKey = p.OrderStatus.ToString().ToLower(),
                PaymentStatus = p.PaymentStatus,
                PaymentStatusKey = p.PaymentStatus.ToString().ToLower(),
                CreatedAt = p.CreatedAt,
                OrderType = p.OrderType,
                Subtotal = p.Subtotal,
                Discount = p.Discount,
                MoneyChange = p.MoneyChange,
                MoneyReceive = p.MoneyReceive,
                Staff = p.Staff,
                OrderDetails = p.OrderDetails.ToList().Select(i => new OrderDetailsVm()
                {
                    Key = i.DishId,
                    OrderId = i.OrderId,
                    DishId = i.DishId,
                    Qty = i.Qty,
                    UnitPrice = i.UnitPrice,
                    Amount = i.Amount,
                    DishNote = i.DishNote,
                    DishName = i.DishName,
                }).ToList(),
            }).ToListAsync();
        }

        public async Task<List<OrderVm>> GetAllBooking()
        {
            var results = await _context.Orders.Where(p => p.OrderType == OrderType.Booking).Include(one => one.OrderDetails).Select(p => new OrderVm()
            {
                Key = p.Id,
                Id = p.Id,
                Total = p.Total,
                Note = p.Note,
                TableId = p.TableId,
                TableName = p.Table.Name,
                OrderStatus = p.OrderStatus,
                OrderStatusKey = p.OrderStatus.ToString().ToLower(),
                PaymentStatus = p.PaymentStatus,
                PaymentStatusKey = p.PaymentStatus.ToString().ToLower(),
                CreatedAt = p.CreatedAt,
                OrderType = p.OrderType,
                Subtotal = p.Subtotal,
                Discount = p.Discount,
                MoneyChange = p.MoneyChange,
                MoneyReceive = p.MoneyReceive,
                Staff = p.Staff,
                OrderDetails = p.OrderDetails.ToList().Select(i => new OrderDetailsVm()
                {
                    Key = i.DishId,
                    OrderId = i.OrderId,
                    DishId = i.DishId,
                    Qty = i.Qty,
                    UnitPrice = i.UnitPrice,
                    Amount = i.Amount,
                    DishNote = i.DishNote,
                    DishName = i.DishName,
                }).ToList(),
            }).ToListAsync();
            return results;
        }

        public async Task<List<OrderVm>> GetCurrentBooking()
        {
            var results = await _context.Orders.Where(p => p.OrderType == OrderType.Booking && (p.OrderStatus == OrderStatus.Ordered || p.OrderStatus == OrderStatus.ToReceive)).Include(one => one.OrderDetails).Select(p => new OrderVm()
            {
                Key = p.Id,
                Id = p.Id,
                Total = p.Total,
                Note = p.Note,
                TableId = p.TableId,
                TableName = p.Table.Name,
                OrderStatus = p.OrderStatus,
                OrderStatusKey = p.OrderStatus.ToString().ToLower(),
                PaymentStatus = p.PaymentStatus,
                PaymentStatusKey = p.PaymentStatus.ToString().ToLower(),
                CreatedAt = p.CreatedAt,
                OrderType = p.OrderType,
                Subtotal = p.Subtotal,
                Discount = p.Discount,
                MoneyChange = p.MoneyChange,
                MoneyReceive = p.MoneyReceive,
                Staff = p.Staff,
                OrderDetails = p.OrderDetails.ToList().Select(i => new OrderDetailsVm()
                {
                    Key = i.DishId,
                    OrderId = i.OrderId,
                    DishId = i.DishId,
                    Qty = i.Qty,
                    UnitPrice = i.UnitPrice,
                    Amount = i.Amount,
                    DishNote = i.DishNote,
                    DishName = i.DishName,
                }).ToList(),
            }).ToListAsync();
            return results;
        }

        public async Task<OrderVm> GetById(string id)
        {
            var target = await _context.Orders.Include(one => one.OrderDetails).Select(p => new OrderVm()
            {
                Id = p.Id,
                Total = p.Total,
                Note = p.Note,
                TableId = p.TableId,
                TableName = p.Table.Name,
                OrderStatus = p.OrderStatus,
                OrderStatusKey = p.OrderStatus.ToString().ToLower(),
                PaymentStatus = p.PaymentStatus,
                PaymentStatusKey = p.PaymentStatus.ToString().ToLower(),
                CreatedAt = p.CreatedAt,
                OrderType = p.OrderType,
                Subtotal = p.Subtotal,
                Discount = p.Discount,
                MoneyChange = p.MoneyChange,
                MoneyReceive = p.MoneyReceive,
                Staff = p.Staff,
                OrderDetails = p.OrderDetails.ToList().Select(i => new OrderDetailsVm()
                {
                    Key = i.DishId,
                    OrderId = i.OrderId,
                    DishId = i.DishId,
                    Qty = i.Qty,
                    UnitPrice = i.UnitPrice,
                    Amount = i.Amount,
                    DishNote = i.DishNote,
                    DishName = i.DishName,
                }).ToList(),
            }).Where(package => package.Id == id).FirstOrDefaultAsync();

            return target;
        }

        public async Task<OrderVm> GetReccentlyOrder(string id)
        {
            var target = await _context.Orders.Include(one => one.OrderDetails).Where(x => x.OrderStatus == OrderStatus.Ordered || x.OrderStatus == OrderStatus.ToReceive).Select(p => new OrderVm()
            {
                Id = p.Id,
                Total = p.Total,
                Note = p.Note,
                TableId = p.TableId,
                TableName = p.Table.Name,
                OrderStatus = p.OrderStatus,
                OrderStatusKey = p.OrderStatus.ToString().ToLower(),
                PaymentStatus = p.PaymentStatus,
                PaymentStatusKey = p.PaymentStatus.ToString().ToLower(),
                CreatedAt = p.CreatedAt,
                OrderType = p.OrderType,
                Subtotal = p.Subtotal,
                Discount = p.Discount,
                MoneyChange = p.MoneyChange,
                MoneyReceive = p.MoneyReceive,
                Staff = p.Staff,
                OrderDetails = p.OrderDetails.ToList().Select(i => new OrderDetailsVm()
                {
                    Key = i.DishId,
                    OrderId = i.OrderId,
                    DishId = i.DishId,
                    Qty = i.Qty,
                    UnitPrice = i.UnitPrice,
                    Amount = i.Amount,
                    DishNote = i.DishNote,
                    DishName = i.DishName,
                }).ToList(),
            }).Where(package => package.TableId == id).FirstOrDefaultAsync();

            return target;
        }

        public async Task<List<RevenueVm>> GetRevenue()
        {
            var result = await _context.Orders.Where(x => x.OrderStatus == OrderStatus.Completed).GroupBy(x => x.CreatedAt.Date).Select(x => new RevenueVm() { Date = x.Key.ToString("MM/dd/yyyy"), Total = x.Sum(o => o.Total) }).ToListAsync();
            return result;
        }

        public async Task<List<TopSellersVm>> GetTopSellers()
        {
            var successfulOrders = await _context.Orders.Include(x => x.OrderDetails).ThenInclude(d => d.Dish).Where(x => x.OrderStatus == OrderStatus.Completed).ToListAsync();

            var result = successfulOrders.SelectMany(x => x.OrderDetails).GroupBy(x => new { x.Dish.Name, MonthYear = new DateTime(x.Order.CreatedAt.Year, x.Order.CreatedAt.Month, 1) }).Select(g => new TopSellersVm
            {
                Name = g.Key.Name,
                Time = g.Key.MonthYear.ToString("MM-yyyy"),
                Total = g.Sum(od => od.Amount)
            }).ToList();

            return result;
        }

        public async Task<CountManagementVm> GetCountManagement()
        {
            var countDish = await _context.Dishes.CountAsync();
            var countMenu = await _context.Menus.CountAsync();
            var countTable = await _context.Tables.CountAsync();
            var countRevenue = await _context.Orders.Where(x => x.OrderStatus == OrderStatus.Completed).SumAsync(x => x.Total);
            var countOrderCompleted = await _context.Orders.Where(x => x.OrderStatus == OrderStatus.Completed).CountAsync();

            return new CountManagementVm() { Dishes = countDish, Menus = countMenu, Tables = countTable, Revenue = countRevenue, OrderCompleted = countOrderCompleted };
        }
    }
}