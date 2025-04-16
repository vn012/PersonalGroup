using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NoteMedia
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int MediaTypeId { get; set; }

        public string Url { get; set; } = string.Empty;
        public string? Metadata { get; set; } 

        // Navegação
        public MediaType MediaType { get; set; } = null!;
        public Note Note { get; set; } = null!;
    }
}
