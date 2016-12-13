using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using notes.Models;

namespace notes.Controllers
{
    public class HomeController : Controller
    {
        private NotesDbContext dbContext;
        private JsonHelper jsonHelper;

        public HomeController()
        {
            dbContext = new NotesDbContext();
            jsonHelper = new JsonHelper();
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
                var note = dbContext.Notes.FirstOrDefault(x => x.NoteId == id);
                if (note == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                ViewBag.text = note.Text;
            }

            return View();
            // TODO: разбить на два метода - get и post
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
                dbContext.Notes.Add(note);
            }
            else
            {
                note = dbContext.Notes.FirstOrDefault(x => x.NoteId == id.Value);
                if (note == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }

            //if (text == null)
            //{
            //    text = "";
            //}

            //note.Text = text;
            if (notearea == null)
            {
                notearea = "";
            }
            note.Text = notearea;
            note.LastChangeDate = DateTime.UtcNow.Ticks;

            dbContext.SaveChanges();
            return RedirectToAction("Notes", new { @id = id });
            //return RedirectToAction("Notes", Guid.NewGuid());
        }
    }
}