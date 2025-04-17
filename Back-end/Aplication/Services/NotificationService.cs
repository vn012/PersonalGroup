using Aplication.DTOs.Notes;
using Aplication.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationHub _notificationHub;

        public NotificationService(INotificationHub notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public async Task NotifyNoteCreatedAsync(ResponseNotesDTO note)
        {
            // aplicar a logia aqui
            await _notificationHub.SendNoteToUserAsync(note.UserId, note);
        }
    }

}
