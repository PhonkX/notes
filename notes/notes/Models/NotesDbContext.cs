using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace notes.Models
{
    public class NotesDbContext: DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Note> Notes { get; set; }
        public IDbSet<Session> Sessions { get; set; }
    }

    public class NotesContextInitializer : DropCreateDatabaseIfModelChanges<NotesDbContext> {}
}