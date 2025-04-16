using Aplication.DTOs.Media;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Notes
{
    public class UpdateNotesDTO
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}
