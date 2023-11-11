using Application.Dishes;
using AutoMapper;
using Data.EF;
using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Models.OrderDetails;
using Models.Orders;

namespace Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly UOrderDbContext _context;
        private readonly IDishService _dishService;
        private readonly IMapper _mapper;

        public OrderService(UOrderDbContext dbContext, IDishService dishService, IMapper mapper)
        {
            _context = dbContext;
            _dishService = dishService;
            _mapper = mapper;
        }

        public async Task<int> Create(OrderCreateRequest req)
        {
            var id = req.Id;
            var item = new Order()
            {
                Id = id,
                Total = req.Total,
                Note = req.Note,
                TableId = req.TableId,
                OrderStatus = req.OrderStatus,
                PaymentStatus = req.PaymentStatus,
                CreatedAt = req.CreatedAt,
                OrderType = req.OrderType,
                Subtotal = req.Subtotal,
                Discount = req.Discount,
            };
            foreach (var child in req.OrderDetails)
            {
                OrderDetail connect = new OrderDetail()
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
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(OrderUpdateRequest req)
        {
            var item = new Order()
            {
                Id = req.Id,
                Total = req.Total,
                Note = req.Note,
                TableId = req.TableId,
                OrderStatus = req.OrderStatus,
                PaymentStatus = req.PaymentStatus,
                CreatedAt = req.CreatedAt,
                OrderType = req.OrderType
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

        public async Task<List<OrderVm>> GetAll()
        {
            return await _context.Orders.Include(one => one.OrderDetails).Select(p => new OrderVm()
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
    }
}