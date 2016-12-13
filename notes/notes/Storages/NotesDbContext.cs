using System;
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

    public class NotesContextInitializer : DropCreateDatabaseIfModelChanges<NotesDbContext>
    {
        protected override void Seed(NotesDbContext context)
        {
            base.Seed(context);

            context.Users.Add(
                new User
                {
                    Login = "PhonkX",
                    Password = "Ololo",
                    UserId = Guid.NewGuid()
                }
            );

            context.SaveChanges();
        }
    }
}