using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Relacionamento com mídias
        public List<NoteMedia> MediaItems { get; set; } = new();
        public List<NoteTags> NoteTags { get; set; } = new();
    }
}
