using Microsoft.AspNetCore.SignalR;
using Models.Tables;

namespace Application.Actions
{
    public class ActionHub : Hub
    {
        public async Task SendCallStaffNotification(TableVm table)
        {
            var userId = Context.UserIdentifier;
            await Clients.User(userId).SendAsync("SendCallStaffNotification", table);
        }
    }
}