using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace notes.Models
{
    public class Note
    {
        [Key]
        public int DbId { get; set; } // TODO: переделать на один ключ

        public Guid NoteId { get; set; }

        public Guid UserId { get; set; }

        public long CreationDate { get; set; }

        public long LastChangeDate { get; set; } // или дата последнего запроса?

        public string Text { get; set; }
    }
}