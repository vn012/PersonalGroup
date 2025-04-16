using Domain.Entities;
using Domain.Interfaces;
using Infra.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class NotesRepository : BaseRepository<Note>, INotesRepository
    {
        public NotesRepository(Context context) : base(context)
        {
        }


        public async Task<IEnumerable<Note>> GetAllByUserAsync(int userId)
        {
             var res = await _context.Note
                .Where(n => n.UserId == userId)
                .ToListAsync();

            return res;
        }


        public async Task<IEnumerable<Note>> GetAllByUserWithTagsAsync(int userId)
        {
            var res =  await _context.Note
                .Where(n => n.UserId == userId)
                .Include(n => n.NoteTags)
                .ToListAsync();

            return res;
        }     
        
        public async Task<IEnumerable<Note>> GetAllWithTagsAndMediaAsync()
        {
            var res =  await _context.Note
                .Include(n => n.NoteTags)
                .ThenInclude(nt => nt.Tag)
                .Include(n => n.MediaItems)
                .ThenInclude(mt => mt.MediaType)
                .Where(n => n.DeletedAt == null)
                .ToListAsync();

            return res;
        }


        public async Task<IEnumerable<NoteMedia?>> GetByLink(string type)
        {
            var res = await _context.NoteMedia
                .AsNoTracking()
                .Include(nm => nm.MediaType) // Opcional, da pra buscar somente pelo ID (com enum)
                .Where(nm => nm.MediaType.Name.ToLower() == type.ToLower()) 
                .OrderByDescending(nm => nm.Id)
                .ToListAsync();

            return res;
        }


        public async Task<Note?> GetByIdWithTagsAndMediaAsync(int noteId)
        {
            var res = await _context.Note
                .AsNoTracking()
                .Include(n => n.NoteTags)
                .ThenInclude(nt => nt.Tag)
                .Include(n => n.MediaItems)
                .ThenInclude(mt => mt.MediaType)
                .FirstOrDefaultAsync(n => n.Id == noteId);

            return res;
        }


    }
}