using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Media
{
    public class CreateNoteMediaDTO
    {
        public int MediaTypeId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Metadata { get; set; }
    }
}
