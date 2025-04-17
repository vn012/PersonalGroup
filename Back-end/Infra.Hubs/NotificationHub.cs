using Aplication.DTOs.Notes;
using Microsoft.AspNetCore.SignalR;
using Aplication.Interfaces;
using Infra.Hubs;

namespace Infra.Hubs
{
    public class NotificationHub : INotificationHub
    {
        private readonly IHubContext<NoteHub> _hubContext;

        public NotificationHub(IHubContext<NoteHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNoteToUserAsync(int userId, ResponseNotesDTO note)
        {
            await _hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveUpdate", note);
        }
    }
}

