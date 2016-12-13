using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace notes.Models
{
    public class CookieHelper
    {
        private NotesDbContext dbContext;

        public CookieHelper()
        {
            dbContext = new NotesDbContext();
        }

        public bool IsCookieValid(HttpCookie cookie) // TODO: подумать насчёт userId!!!
        {
            var sessiondId = Guid.Parse(cookie.Values["SID"]); // TODO: подумать насчёт TryParse
            var session = dbContext.Sessions.FirstOrDefault(x => x.SessionId == sessiondId);
            if (session == null)
            {
                return false;
            }

            if (cookie.Expires.Ticks != session.LastRequestDate)
                // TODO: подумать, может, от CreationDate отсчитывать; а ещё проверить, какой часовой пояс там
            {
                return false;
            }

            return true;
        }
    }
}