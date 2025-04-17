using Aplication.DTOs.Notes;
using Aplication.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Note> _noteBaseRepository;
        private readonly IBaseRepository<NoteTags> _noteTagsBaseRepository;
        private readonly IBaseRepository<NoteMedia> _MediaBaseRepository;
        private readonly IMapper _mapper;

        public NotesService(INotesRepository notesRepository, IUnitOfWork unitOfWork, IBaseRepository<Note> noteBaseRepository, IBaseRepository<NoteTags> noteTagsBaseRepository, IBaseRepository<NoteMedia> mediaBaseRepository, IMapper mapper)
        {
            _notesRepository = notesRepository;
            _unitOfWork = unitOfWork;
            _noteBaseRepository = noteBaseRepository;
            _noteTagsBaseRepository = noteTagsBaseRepository;
            _MediaBaseRepository = mediaBaseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResponseNotesDTO>> GetOnlyNotes()
        {
            var notesDB = await _noteBaseRepository.GetAllAsync();
            notesDB = notesDB.Where(n => n.DeletedAt == null);

            return _mapper.Map<IEnumerable<ResponseNotesDTO>>(notesDB);
        }    

        public async Task<IEnumerable<ResponseNotesDTO>> GetNotes()
        {
            try
            {
                var notesDB = await _notesRepository.GetAllWithTagsAndMediaAsync();
                notesDB = notesDB.Where(n => n.DeletedAt == null).ToList().OrderByDescending(n => n.Id) ; //isso é responsabilidade do repositorio, debugar com dados reais depois

                return _mapper.Map<IEnumerable<ResponseNotesDTO>>(notesDB);
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseNotesDTO> GetNotesById()
        {
            try
            {
                var noteDB = await _notesRepository.GetAllWithTagsAndMediaAsync();

                return _mapper.Map<ResponseNotesDTO>(noteDB);
            }catch(Exception ex)
            {
                throw ex;
            }

        }

        #region ADD
        public async Task<ResponseNotesDTO> AddNoteWithTagsAndMediaAsync(CreateNotesDTO createNoteDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Note
                var noteEntity = _mapper.Map<Note>(createNoteDto);
                noteEntity.CreatedAt = DateTime.UtcNow;
                noteEntity.UpdatedAt = DateTime.UtcNow;

                await _noteBaseRepository.AddAsync(noteEntity);
                await _unitOfWork.SaveChangesAsync();

                // NoteTags
                if (createNoteDto.Tags?.Any() == true)
                {
                    var noteTagsList = new List<NoteTags>();
                    foreach (var tag in createNoteDto.Tags)
                    {
                        noteTagsList.Add(new NoteTags
                        {
                            TagId = tag.Id,
                            NoteId = noteEntity.Id,
                        });
                    }
                
                    await _noteTagsBaseRepository.AddRangeAsync(noteTagsList);
                }

                // Media
                if (createNoteDto.MediaItems?.Any() == true)
                {
                    var mediaItemsList = new List<NoteMedia>();
                    foreach(var mediaItem in createNoteDto.MediaItems)
                    {
                       mediaItemsList.Add(new NoteMedia
                       {
                            NoteId = noteEntity.Id,
                            MediaTypeId =  mediaItem.MediaTypeId,
                            Url = mediaItem.Url,
                            Metadata = mediaItem.Metadata
                       });

                    }
                    
                    await _MediaBaseRepository.AddRangeAsync(mediaItemsList);
                }

                await _unitOfWork.CommitAsync();
                return _mapper.Map<ResponseNotesDTO>(noteEntity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        Task<IEnumerable<ResponseNotesDTO>> INotesService.GetNotesById()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
