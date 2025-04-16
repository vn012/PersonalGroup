using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetAllWithTagsAndMediaAsync();
        Task<IEnumerable<Note>> GetAllByUserAsync(int userId);
        Task<IEnumerable<Note>> GetAllByUserWithTagsAsync(int userId);
        Task<IEnumerable<NoteMedia?>> GetByLink(string type);
        Task<Note?> GetByIdWithTagsAndMediaAsync(int noteId);


    }
}
