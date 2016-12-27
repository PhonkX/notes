using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using notes.Models;

namespace notes.Storages
{
    public class DatabaseConnector // TODO: разнести по разным хранилищам
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

        public IEnumerable<Note> SearchNotes(Func<Note, bool> filter) // TODO: подумать, может, объединить методы
        {
            return dbContext.Notes.Where(filter); 
        }

        public void AddUser(User user)
        {
            dbContext.Users.Add(user);
        }

        public void AddNote(Note note)
        {
            dbContext.Notes.Add(note);
        }

        public void AddSession(Session session)
        {
            dbContext.Sessions.Add(session);
        }

        public void Save()  // TODO: подумать про то, чтобы обойтись без такого
        {
            dbContext.SaveChanges();
        }
    }
}