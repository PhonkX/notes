using System;
using System.Web;
using notes.Models;

namespace notes.Authentication
{
    public interface IAuthenticator
    {
        bool IsUserAuthenticated(HttpCookie cookie, Guid userId);
        Session AuthenticateUser(string login, string password);
    }
}