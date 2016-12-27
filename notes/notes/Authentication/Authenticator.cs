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
        private DatabaseConnector dbConnector;
        private SessionManager sessionManager;

        public Authenticator()
        {
            dbConnector = new DatabaseConnector();
            sessionManager = new SessionManager();
        }

        public bool IsUserAuthenticated(HttpCookie cookie, Guid userId) // TODO: проверять userId
        {
            throw new NotImplementedException(); // TODO: подумать, что тут лучше делать
        }

        public Session AuthenticateUser(string login, string password) // TODO: подумать о кодах возврата
        {
            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var user = dbConnector.SearchUser(x => x.Login == login);
            if (user == null)
            {
                return null; // TODO: подумать, что возвращать (по идее, страницу с надписью о неверных данных)
            }

            if (user.Password != password)
            {
                return null;
            }

            return sessionManager.CreateSession(user.UserId); // TODO: подумать, нужны ли какие-то проверки здесь
        }
    }
}