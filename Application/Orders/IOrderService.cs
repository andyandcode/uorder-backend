using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Models.Orders;

namespace Application.Orders
{
    public interface IOrderService
    {
        Task<int> Create(OrderCreateRequest req);

        Task<int> Update(OrderUpdateRequest req);

        Task<int> Delete(string id);

        Task<List<OrderVm>> GetAll();

        Task<OrderVm> GetById(string id);

        Task<int> UpdateOrderStatus(string id, JsonPatchDocument<Order> patchDoc);
    }
}