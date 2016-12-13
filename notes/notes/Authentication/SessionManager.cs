﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using notes.Models;
using notes.Storages;

namespace notes.Authentication
{
    public class SessionManager
    {
        private DatabaseConnector dbConnector;

        public Session CreateSession(Guid userId)
        {
            var session = new Session
            {
                SessionId = new Guid(),
                UserId = userId,
                CreationDate = DateTime.UtcNow.Ticks,
                LastRequestDate = DateTime.UtcNow.Ticks
            };
            
            dbConnector.AddSession(session);

            return session;
        }

        public Session FindSession(Guid sessionId)
        {
            return dbConnector.SearchSession(x => x.SessionId == sessionId);
        }
    }
}