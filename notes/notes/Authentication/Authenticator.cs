using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using notes.Models;
using notes.Storages;

namespace notes.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private NotesDbContext dbContext;

        public Authenticator()
        {
            dbContext = new NotesDbContext();
        }

        public bool IsUserAuthenticated() // TODO: проверять userId
        {
            throw new NotImplementedException(); // TODO: подумать, что тут лучше делать
        }
    }
}