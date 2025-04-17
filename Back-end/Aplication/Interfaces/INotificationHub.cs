using Aplication.DTOs.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface INotificationHub
    {
        Task SendNoteToUserAsync(int userId, ResponseNotesDTO note);
    }
}
