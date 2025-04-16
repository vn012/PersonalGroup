using Aplication.DTOs.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Repositories
{
    public interface INotesService
    {
        Task<IEnumerable<ResponseNotesDTO>> GetNotes();
        Task<IEnumerable<ResponseNotesDTO>> GetNotesById();
        Task<ResponseNotesDTO> AddNoteWithTagsAndMediaAsync(CreateNotesDTO noteDTO);
    }
}
