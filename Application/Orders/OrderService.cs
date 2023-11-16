using Application.Dishes;
using Application.SystemSettings;
using AutoMapper;
using Data.EF;
using Data.Entities;
using Data.Enums;
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
        private readonly ISystemSettingService _systemSettingService;
        private readonly IMapper _mapper;

        public OrderService(UOrderDbContext dbContext, IDishService dishService, IMapper mapper, ISystemSettingService systemSettingService)
        {
            _context = dbContext;
            _dishService = dishService;
            _mapper = mapper;
            _systemSettingService = systemSettingService;
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
                CompletedAt = await OrderInQueue(req),
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
                CompletedAt = p.CompletedAt,
                TimeToReceive = CalculatorTimeToReceive(p.CompletedAt),
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
                CompletedAt = p.CompletedAt,
                TimeToReceive = CalculatorTimeToReceive(p.CompletedAt),
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
                CompletedAt = p.CompletedAt,
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

        private async Task<DateTime> OrderInQueue(OrderCreateRequest req)
        {
            var settings = await _systemSettingService.GetSettings();

            List<int> listTimes = new List<int>();
            foreach (var item in req.OrderDetails)
            {
                var dish = await _dishService.GetById(item.DishId);
                listTimes.Add(dish.CompletionTime * item.Qty);
            }

            var firstOrder = _context.Orders.ToList().OrderByDescending(x => x.CreatedAt).Where(x => x.OrderStatus == OrderStatus.Ordered || x.OrderStatus == OrderStatus.ToReceive).FirstOrDefault();

            int minCompletionTime = ScheduleJobs(listTimes, settings.ChefCount);

            DateTime currentTime = DateTime.Now;

            if (firstOrder == null)
            { currentTime = DateTime.Now; }
            else
            { currentTime = firstOrder.CompletedAt; }

            TimeSpan additionalTime = TimeSpan.FromMinutes(minCompletionTime);

            DateTime newTime = currentTime.Add(additionalTime);

            return newTime;
        }

        private static int CalculatorTimeToReceive(DateTime target)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeDifference = target.Subtract(now);
            return Convert.ToInt32(timeDifference.TotalMilliseconds);
        }

        private static int ScheduleJobs(List<int> jobTimes, int numWorkers)
        {
            List<List<int>> schedule = new List<List<int>>(numWorkers);
            for (int i = 0; i < numWorkers; i++)
            {
                schedule.Add(new List<int>());
            }

            // Sắp xếp các công việc theo thời gian hoàn thành tăng dần
            jobTimes = jobTimes.OrderByDescending(time => time).ToList();

            // Thêm công việc vào công nhân có thời gian hoàn thành ngắn nhất
            foreach (var jobTime in jobTimes)
            {
                int minIndex = GetMinIndex(schedule);
                schedule[minIndex].Add(jobTime);
            }

            // Tính thời gian hoàn thành ngắn nhất
            int minCompletionTime = schedule.Max(worker => worker.Sum());

            return minCompletionTime;
        }

        private static int GetMinIndex(List<List<int>> schedule)
        {
            int minIndex = 0;
            int minValue = schedule[0].Sum();

            for (int i = 1; i < schedule.Count; i++)
            {
                int currentValue = schedule[i].Sum();
                if (currentValue < minValue)
                {
                    minIndex = i;
                    minValue = currentValue;
                }
            }

            return minIndex;
        }
    }
}