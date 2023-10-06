using Models.Orders;

namespace Application.Orders
{
    public interface IOrderService
    {
        Task<int> Create(OrderCreateRequest req);

        Task<int> Update(OrderUpdateRequest req);

        Task<int> Delete(string id);

        Task<List<OrderVm>> GetAllOrder();

        Task<OrderVm> GetOrderById(string id);
    }
}