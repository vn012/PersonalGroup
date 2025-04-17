using Aplication.DTOs.Notes;
using Aplication.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace PersonalGroupAPI.Hubs
{
    public class NotificationHubAdapter : INotificationHub
    {
        private readonly IHubContext<NoteHub> _hubContext;

        public NotificationHubAdapter(IHubContext<NoteHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNoteToUserAsync(int userId, ResponseNotesDTO note)
        {
            await _hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveUpdate", note);
        }
    }
}
