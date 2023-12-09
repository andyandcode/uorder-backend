using Models.Orders;

namespace Application.Payment
{
    public interface IPaymentService
    {
        Task GetAllPayment();

        string VnPayPayment(OrderCreateRequest order, string id);

        string VnPayPayOrder(OrderVm order);

        Task<string> ZaloPay(OrderCreateRequest order, string id);
    }
}