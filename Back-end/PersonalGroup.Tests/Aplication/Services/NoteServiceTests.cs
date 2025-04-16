using Xunit;
using Moq;
using Aplication.Services;
using Aplication.Mapping;
using Aplication.DTOs;

using Aplication.DTOs.Notes;
using Domain.Interfaces;
using AutoMapper;
using Domain.Entities;
using Aplication.DTOs.Media;
using Infra.Repositories;
using Aplication.DTOs.Tags;
using System.Xml.Linq;

namespace PersonalGroup.Tests.Aplication.Services
{
    public class NoteServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<INotesRepository> _mockNoteRepo;
        private readonly Mock<IBaseRepository<Note>> _mockNoteBaseRepo;
        private readonly Mock<IBaseRepository<NoteTags>> _mockNoteTagsBaseRepo;
        private readonly Mock<IBaseRepository<NoteMedia>> _mockMediaBaseRepo;
        private readonly IMapper _mapper;

        private readonly NotesService _notesService;

        public NoteServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockNoteRepo = new Mock<INotesRepository>();
            _mockNoteBaseRepo = new Mock<IBaseRepository<Note>>();
            _mockNoteTagsBaseRepo = new Mock<IBaseRepository<NoteTags>>();
            _mockMediaBaseRepo = new Mock<IBaseRepository<NoteMedia>>(); 


            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<NotesProfile>();
            });
            _mapper = config.CreateMapper();

            _notesService = new NotesService(
                _mockNoteRepo.Object,
                _mockUnitOfWork.Object,
                _mockNoteBaseRepo.Object,
                _mockNoteTagsBaseRepo.Object,
                _mockMediaBaseRepo.Object, 
                _mapper
            );
        }

        [Fact]
        public async Task GetAllNotes_ShouldReturnAllNotes()
        {
            // Arrange
            var notesMock = new List<Note>
            {
                new Note { Id = 1, Text = "Note 1", DeletedAt = null },
                new Note { Id = 2, Text = "Note 2", DeletedAt = DateTime.UtcNow }, 
                new Note { Id = 3, Text = "Note 3", DeletedAt = null },
            };

            _mockNoteBaseRepo
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(notesMock);

            //Act
            var result = await _notesService.GetOnlyNotes();

            // Assert
            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count); 
            Assert.DoesNotContain(resultList, n => n.Id == 2); 
            Assert.Contains(resultList, n => n.Id == 1);
            Assert.Contains(resultList, n => n.Id == 3);
        }

        [Fact]
        public async Task GetAllNotes_ShouldReturnAllNotes_WithTagsAndMedia()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag{ Id = 1, Name = "organizacao"},
                new Tag{ Id = 2, Name = "generico"}
            };
            var noteTags1 = new List<NoteTags> { new NoteTags { NoteId = 1, TagId = 1, Tag = tags[0] } };
            var noteTags2 = new List<NoteTags> { new NoteTags { NoteId = 2, TagId = 1, Tag = tags[0] } };
            var noteTags3 = new List<NoteTags> { new NoteTags { NoteId = 3, TagId = 2, Tag = tags[1] } };
      
            var mediaType = new List<MediaType>
            {
                new MediaType{  Id = 1,Name = "Link" },
                new MediaType{  Id = 2,Name = "Fotos" },

            };
            var mediaItems1 = new List<NoteMedia> { new NoteMedia { Id = 1, NoteId = 1, Url = "https://trello.com", MediaType = mediaType[0] }, };
            var mediaItems2 = new List<NoteMedia> { new NoteMedia { Id = 2, NoteId = 3, Url = "https://trello.com", MediaType = mediaType[1] } };
               
            
            var notesDTO = new List<Note>
            {
                new Note { Id = 1, Text = "Note 1", NoteTags = noteTags1, MediaItems = mediaItems1, DeletedAt = null },
                new Note { Id = 2, Text = "Note 2", NoteTags = noteTags2, MediaItems = mediaItems1, DeletedAt = DateTime.UtcNow },
                new Note { Id = 3, Text = "Note 3", NoteTags = noteTags3, MediaItems = mediaItems2, DeletedAt = null },
            };


            _mockNoteRepo
                .Setup(repo => repo.GetAllWithTagsAndMediaAsync())
                .ReturnsAsync(notesDTO);


            //Act 
            var resultList = await _notesService.GetNotes();

            // Assert
            Assert.NotNull(resultList);
            Assert.Equal(2, resultList.Count());
            Assert.DoesNotContain(resultList, n => n.Id == 2);
            Assert.Contains(resultList, n => n.Id == 1);
            Assert.Contains(resultList, n => n.Id == 3);

            foreach (var note in resultList)
            {
                Assert.NotNull(note.Tags);
                Assert.NotEmpty(note.Tags);

                Assert.NotNull(note.MediaItems);
                Assert.NotEmpty(note.MediaItems);

            }

            var note1 = resultList.First(n => n.Id == 1);
            Assert.Equal("https://trello.com", note1.MediaItems.FirstOrDefault().Url);
            Assert.Equal("organizacao", note1.Tags.First().Name);
            Assert.Equal("Link", note1.MediaItems.First().Type);
        }

        [Fact]
        public async Task AddNoteWithTagsAndMediaAsync_ShouldAddNoteWithTagsAndMedia()
        {
            // Arrange
            var createNoteDto = new CreateNotesDTO
            {
                Text = "Nova Nota",
                UserId = 1,
                Tags = new List<TagDTO>
                {
                    new TagDTO { Id = 1, Name = "Trabalho" }
                },
                MediaItems = new List<CreateNoteMediaDTO>
                {
                    new CreateNoteMediaDTO { Url = "https://link.com", MediaTypeId = 1 }
                }
            };
            var noteEntity = _mapper.Map<Note>(createNoteDto);

            _mockNoteBaseRepo
                .Setup(r => r.AddAsync(It.IsAny<Note>()))
                .Returns(Task.CompletedTask);

            _mockNoteTagsBaseRepo
                .Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<NoteTags>>()))
                .Returns(Task.CompletedTask);

            _mockMediaBaseRepo
                .Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<NoteMedia>>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(u => u.BeginTransactionAsync())
                .Returns(Task.CompletedTask);
            //_mockUnitOfWork
            //    .Setup(u => u.SaveChangesAsync())
            //    .Returns(Task.CompletedTask);
            _mockUnitOfWork
                .Setup(u => u.CommitAsync())
                .Returns(Task.CompletedTask);


            // Act
            var result = await _notesService.AddNoteWithTagsAndMediaAsync(createNoteDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Nova Nota", result.Text);
            //Assert.Equal(1, result.Id);

            _mockNoteBaseRepo.Verify(r => r.AddAsync(It.IsAny<Note>()), Times.Once);
            _mockNoteTagsBaseRepo.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<NoteTags>>()), Times.Once);
            _mockMediaBaseRepo.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<NoteMedia>>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

    }
}
