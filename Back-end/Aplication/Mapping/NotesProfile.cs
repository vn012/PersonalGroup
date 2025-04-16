using AutoMapper;
using Aplication.DTOs.Notes;
using Aplication.DTOs.Media;
using Domain.Entities;
using System.Linq;
using Aplication.DTOs.Tags;

namespace Aplication.Mapping
{
    public class NotesProfile : Profile
    {
        public NotesProfile()
        {
            // Create
            CreateMap<CreateNotesDTO, Note>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.NoteTags, opt => opt.Ignore())
                .ForMember(dest => dest.MediaItems, opt => opt.Ignore());

            CreateMap<CreateNoteMediaDTO, NoteMedia>();

            // Update
            CreateMap<UpdateNotesDTO, Note>()
                .ForMember(dest => dest.NoteTags, opt => opt.Ignore())
                .ForMember(dest => dest.MediaItems, opt => opt.Ignore());

            // Response
            CreateMap<Note, ResponseNotesDTO>()
                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src => src.NoteTags.Select(nt => nt.Tag)))
                .ForMember(dest => dest.MediaItems,
                    opt => opt.MapFrom(src => src.MediaItems));

            CreateMap<NoteMedia, ResponseNoteMediaDTO>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => src.MediaType.Name));

            CreateMap<Tag, TagDTO>();
        }
    }
}
