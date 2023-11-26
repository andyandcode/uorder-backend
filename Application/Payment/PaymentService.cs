using Application.SystemSettings;
using Microsoft.AspNetCore.Http;
using Models.Orders;
using System.Net;

namespace Application.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly ISystemSettingService _systemSettingService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentService(ISystemSettingService systemSettingService, IHttpContextAccessor httpContextAccessor)
        {
            _systemSettingService = systemSettingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task GetAllPayment()
        { return Task.CompletedTask; }

        public string VnPayPayment(OrderCreateRequest order, string id)
        {
            string vnp_Returnurl = _systemSettingService.GetSettings().Result.Domain + "/booking/" + id + "/successfully/";
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_TmnCode = "MQYAZG9R";
            string vnp_HashSecret = "VOUXHERQAUCCBCKMYAQEQTJBTOPDEGFM";

            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Total * 100).ToString());
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedAt.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + id);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", id.ToString());

            return vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
        }

        private string GetIpAddress()
        {
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = IPAddress.Parse(_httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"]);
            }

            return ipAddress.ToString();
        }
    }
}