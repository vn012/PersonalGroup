using Microsoft.AspNetCore.SignalR;
using Aplication.DTOs;
using Aplication.Interfaces;
using Aplication.Interfaces.Repositories;
using Aplication.DTOs.Notes;

namespace PersonalGroupAPI.Hubs
{
    public class NoteHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }
    }
}
