using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using notes.Authentication;
using notes.Models;
using notes.Storages;

namespace notes.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseConnector dbConnector;
        private IAuthenticator authenticator;

        public HomeController()
        {
            dbConnector = new DatabaseConnector();
            authenticator = new Authenticator();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NotesList(Session session)
        {
            var notes = dbConnector.SearchNotes(x => x.UserId == session.UserId);
            ViewBag.NotesList = notes;
            return View();
        }

        [HttpGet]
        public ActionResult Notes(Session session, Guid? id)
        {
            var userId = session.UserId;

            ViewBag.Message = "Note page.";
            ViewBag.UserId = userId;
            ViewBag.NoteId = id;

            if (id.HasValue)
            {
                var note = dbConnector.SearchNote(x => x.NoteId == id && x.UserId == userId);
                if (note == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                ViewBag.text = note.Text;
            }

            return View();
        }

        [HttpPost]
        public ActionResult SaveNote(Guid? id, Guid userId, string notearea) // TODO: переименовать параметр
        {
            Note note;
            if (!id.HasValue)
            {
                id = Guid.NewGuid();
                note = new Note
                {
                    NoteId = id.Value,
                    UserId = userId,
                    CreationDate = DateTime.UtcNow.Ticks
                };
                dbConnector.AddNote(note);
            }
            else
            {
                note = dbConnector.SearchNote(x => x.NoteId == id.Value);
                if (note == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }

            if (notearea == null)
            {
                notearea = "";
            }
            note.Text = notearea;
            note.LastChangeDate = DateTime.UtcNow.Ticks;

            dbConnector.Save();
            return RedirectToAction("Notes", new {@id = id});
        }

        public ActionResult AuthPage()
        {
            var backUrl = HttpContext.Request.Params["backUrl"];

            ViewBag.BackUrl = backUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Auth(string login, string password, string backUrl) // TODO: разнести по разным контроллерам
        {
            var session = authenticator.AuthenticateUser(login, password);
            if (session == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); // TODO: переделать на возврат страницы
            }
            // TODO: доделать
            var cookie = new HttpCookie("SID", session.SessionId.ToString());
            cookie.Expires = DateTime.UtcNow.AddDays(30);
            HttpContext.Response.Cookies.Add(cookie); // TODO: вынести добавление кук в CookieHelper
            return new RedirectResult(backUrl);
        }

        [HttpGet]
        public ActionResult RegisterPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string login, string password)
        {
            var user = dbConnector.SearchUser(x => x.Login == login);
            if (user != null)
            {
                return RedirectToAction("RegisterPage");
            }

            dbConnector.AddUser(
                new User
                {
                    Login = login,
                    Password = password,
                    UserId = Guid.NewGuid()
                });

            return RedirectToAction("Index");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            if (exception.GetType() == typeof(AuthenticationErrorException))
            {
                filterContext.Result = new RedirectResult("/Home/AuthPage?backUrl=" + filterContext.HttpContext.Request.Path); //TODO: впилить обработку этого параметра (впилить скрытый инпут с этим адресом)
            }
        }
    }
}