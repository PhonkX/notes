using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace notes.Models
{
    public class Session
    {
        [Key]
        public int DbId { get; set; }

        public Guid SessionId { get; set; }

        public Guid UserId { get; set; }

        public long CreationDate { get; set; }

        public long LastRequestDate { get; set; }
    }
}