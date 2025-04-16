using Aplication.DTOs.Media;
using Aplication.DTOs.Tags;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Notes
{
    public class ResponseNotesDTO
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<TagDTO> Tags { get; set; } = new();
        public List<ResponseNoteMediaDTO> MediaItems { get; set; } = new();

    }
}
