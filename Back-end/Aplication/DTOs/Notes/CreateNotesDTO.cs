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
    public class CreateNotesDTO
    {
        public int UserId { get; set; }
        public string? Text { get; set; }

        public List<TagDTO>? Tags { get; set; } = new();
        public List<CreateNoteMediaDTO>? MediaItems { get; set; } = new();
    }
}