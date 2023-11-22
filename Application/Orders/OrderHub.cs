using Microsoft.AspNetCore.SignalR;

namespace Application.Orders
{
    public class OrderHub : Hub
    {
        public async Task SendOrderNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveOrderNotification", message);
        }
    }
}