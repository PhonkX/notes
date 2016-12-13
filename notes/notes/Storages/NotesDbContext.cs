using System.Data.Entity;
using notes.Models;

namespace notes.Storages
{
    public class NotesDbContext: DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Note> Notes { get; set; }
        public IDbSet<Session> Sessions { get; set; }
    }

    public class NotesContextInitializer : DropCreateDatabaseIfModelChanges<NotesDbContext> {}
}