using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using notes.Models;
using notes.Storages;

namespace notes.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseConnector dbConnector;
        private CookieHelper cookieHelper;

        public HomeController()
        {
            dbConnector = new DatabaseConnector();
            cookieHelper = new CookieHelper();
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

        [HttpGet]
        public ActionResult Notes(Guid? id)
        {
            ViewBag.Message = "Note page.";
            
            ViewBag.NoteId = id;

            if (id.HasValue)
            {
                var note = dbConnector.SearchNote(x => x.NoteId == id);
                if (note == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                ViewBag.text = note.Text;
            }

            return View();
        }

        [HttpPost]
        public ActionResult SaveNote(Guid? id, string notearea) // TODO: переименовать параметр
        {
            Note note;
            if (!id.HasValue)
            {
                id = Guid.NewGuid();
                note = new Note
                {
                    NoteId = id.Value,
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
            return RedirectToAction("Notes", new { @id = id });
        }

        public ActionResult AuthPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Auth(string login, string password) // TODO: разнести по разным контроллерам
        {
            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(password))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var user = dbConnector.SearchUser(x => x.Login == login);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound); // TODO: подумать, что возвращать (по идее, страницу с надписью о неверных данных)
            }

            if (user.Password != password)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            // TODO: доделать

            return RedirectToAction("Notes");
        }
    }
}