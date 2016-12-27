using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using notes.Authentication;
using notes.Storages;

namespace notes.Models
{
    public class CookieHelper
    {
        private SessionManager sessionManager; // TODO: поменять здесь

        public CookieHelper()
        {
            sessionManager = new SessionManager();
        }

        public bool IsCookieValid(HttpCookie cookie) // TODO: подумать насчёт userId!!!
        {
            var sessionId = Guid.Parse(cookie.Values["SID"]); // TODO: подумать насчёт TryParse и насчёт того, чтобы проверка сессии вытащить куда-нибдь ещё
            var session = sessionManager.FindSession(sessionId);
            if (session == null)
            {
                return false;
            }

            if (cookie.Expires.Ticks <= DateTime.UtcNow.Ticks || session.CreationDate <= DateTime.UtcNow.Ticks)
                // TODO: подумать, может, от CreationDate отсчитывать; а ещё проверить, какой часовой пояс там
            {
                return false;
            }

            return true;
        }
    }
}