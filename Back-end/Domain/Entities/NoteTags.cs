using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NoteTags
    {
        public int NoteId { get; set; }
        public int TagId { get; set; }
        public Note Note { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
