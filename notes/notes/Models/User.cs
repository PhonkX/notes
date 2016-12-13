using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace notes.Models
{
    public class User
    {
        [Key]
        public int DbKey { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public Guid UserId { get; set; }
    }
}