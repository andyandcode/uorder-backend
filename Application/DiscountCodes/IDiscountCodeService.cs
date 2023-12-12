using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Models.DiscountCodes;
using Models.Orders;

namespace Application.DiscountCodes
{
    public interface IDiscountCodeService
    {
        Task<List<DiscountCodeVm>> GetAll();

        Task<int> Create(DiscountCodeCreateRequest req);

        Task<int> Delete(string id);

        Task HardDelete(string itemId);

        Task<int> UndoDelete(string itemId);

        Task<List<DiscountCodeVm>> ReturnAvailableCodes(CartItemVm vm);

        Task<int> ApplyDiscountCode(OrderCreateRequest req);

        Task<int> UpdatePatch(string id, JsonPatchDocument<DiscountCode> patchDoc);

        Task<DiscountCodeVm> GetById(string id);
    }
}