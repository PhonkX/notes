using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using notes.Models;

namespace notes.Storages
{
    public class DatabaseConnector // TODO: подумать, как унифицировать
    {
        private NotesDbContext dbContext;

        public DatabaseConnector()
        {
            dbContext = new NotesDbContext();
        }

        public User SearchUser(Func<User, bool> filter) 
        {
            return dbContext.Users.FirstOrDefault(filter);
        }

        public Session SearchSession(Func<Session, bool> filter)
        {
            return dbContext.Sessions.FirstOrDefault(filter);
        }

        public Note SearchNote(Func<Note, bool> filter)
        {
            return dbContext.Notes.FirstOrDefault(filter);
        }
    }
}