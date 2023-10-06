using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Orders;

namespace Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly UOrderDbContext _context;

        public OrderService(UOrderDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> Create(OrderCreateRequest req)
        {
            try
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
                };
                _context.Add(item);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(OrderUpdateRequest req)
        {
            try
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
                };
                _context.Update(item);
            }
            catch (Exception ex)
            {
                return 0;
            }
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
            var list = await _context.Orders.ToListAsync();
            return list.Select(p => new OrderVm()
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
            }).ToList();
        }

        public async Task<OrderVm> GetOrderById(string id)
        {
            var target = await _context.Orders.FindAsync(id);

            var item = new OrderVm()
            {
                Id = target.Id,
                Total = target.Total,
                Note = target.Note,
                TableId = target.TableId,
                //TableName = target.Table.Name,
                OrderStatus = target.OrderStatus,
                OrderStatusKey = target.OrderStatus.ToString().ToLower(),
                PaymentStatus = target.PaymentStatus,
                PaymentStatusKey = target.PaymentStatus.ToString().ToLower(),
                CreatedAt = target.CreatedAt,
            };

            return item;
        }
    }
}