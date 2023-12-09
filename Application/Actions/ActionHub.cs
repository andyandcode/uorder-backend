using Microsoft.AspNetCore.SignalR;
using Models.Tables;

namespace Application.Actions
{
    public class ActionHub : Hub
    {
        public async Task SendCallStaffNotification(TableVm table)
        {
            await Clients.All.SendAsync("SendCallStaffNotification", table);
        }
    }
}