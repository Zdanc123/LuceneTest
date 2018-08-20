using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuceneTest.Models;

namespace LuceneTest.Controllers
{
    public class CharacterEpisodesController : Controller
    {
        private LuceneContext db = new LuceneContext();

        // GET: CharacterEpisodes
        public ActionResult Index()
        {
            var characterEpisodes = db.CharacterEpisodes.Include(c => c.Character).Include(c => c.Episode);
            return View(characterEpisodes.ToList());
        }

        // GET: CharacterEpisodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterEpisode characterEpisode = db.CharacterEpisodes.Find(id);
            if (characterEpisode == null)
            {
                return HttpNotFound();
            }
            return View(characterEpisode);
        }

        // GET: CharacterEpisodes/Create
        public ActionResult Create()
        {
            ViewBag.CharacterID = new SelectList(db.Characters, "Id", "Name");
            ViewBag.EpisodeID = new SelectList(db.Episodes, "Id", "Name");
            return View();
        }

        // POST: CharacterEpisodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CharacterID,EpisodeID")] CharacterEpisode characterEpisode)
        {
            if (ModelState.IsValid)
            {
                db.CharacterEpisodes.Add(characterEpisode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CharacterID = new SelectList(db.Characters, "Id", "Name", characterEpisode.CharacterID);
            ViewBag.EpisodeID = new SelectList(db.Episodes, "Id", "Name", characterEpisode.EpisodeID);
            return View(characterEpisode);
        }

        // GET: CharacterEpisodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterEpisode characterEpisode = db.CharacterEpisodes.Find(id);
            if (characterEpisode == null)
            {
                return HttpNotFound();
            }
            ViewBag.CharacterID = new SelectList(db.Characters, "Id", "Name", characterEpisode.CharacterID);
            ViewBag.EpisodeID = new SelectList(db.Episodes, "Id", "Name", characterEpisode.EpisodeID);
            return View(characterEpisode);
        }

        // POST: CharacterEpisodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CharacterID,EpisodeID")] CharacterEpisode characterEpisode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(characterEpisode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CharacterID = new SelectList(db.Characters, "Id", "Name", characterEpisode.CharacterID);
            ViewBag.EpisodeID = new SelectList(db.Episodes, "Id", "Name", characterEpisode.EpisodeID);
            return View(characterEpisode);
        }

        // GET: CharacterEpisodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterEpisode characterEpisode = db.CharacterEpisodes.Find(id);
            if (characterEpisode == null)
            {
                return HttpNotFound();
            }
            return View(characterEpisode);
        }

        // POST: CharacterEpisodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterEpisode characterEpisode = db.CharacterEpisodes.Find(id);
            db.CharacterEpisodes.Remove(characterEpisode);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
