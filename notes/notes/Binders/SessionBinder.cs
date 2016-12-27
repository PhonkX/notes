using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using notes.Authentication;
using notes.Models;

namespace notes.Binders
{
    public class SessionBinder: IModelBinder
    {
        private SessionManager sessionManager;

        public SessionBinder()
        {
            sessionManager = new SessionManager();
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cookie = controllerContext.HttpContext.Request.Cookies["SID"];
            if (cookie == null || String.IsNullOrWhiteSpace(cookie.Value))
            {
                throw new AuthenticationErrorException(); // TODO: подумать над тем, чтобы кидать AuthenticationException
            }
            var sid = Guid.Parse(cookie.Value); //TODO: подумать про TryParse
            var session = sessionManager.FindSession(sid);
            if (session == null)
            {
                throw new AuthenticationErrorException();
            }

            return session;
        }
    }
}