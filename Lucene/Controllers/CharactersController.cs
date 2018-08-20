using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CharactersController : Controller
    {
        private CharacterContext db = new CharacterContext();

        // GET: Characters
        public ActionResult Index(string searchString)
        {
            var lista = db.Characters.ToList();
            //List<Character> lista = new List<Character>
            //{
            //    new Character{CharacterID=1, Name="Skywalker", },
            //    new Character{CharacterID=2, Name="Aragorn", },
            //    new Character{CharacterID=3, Name="luke Cage" }


            //};

            //string strIndexDir = @"C:\Users\zdanc\Desktop\test2.txt";
            //Lucene.Net.Store.Directory indexDir = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(strIndexDir));
            //Analyzer std = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29); //Version parameter is used for backward compatibility. Stop words can also be passed to avoid indexing certain words
            //IndexWriter idxw = new IndexWriter(indexDir, std, true, IndexWriter.MaxFieldLength.UNLIMITED); //Create an Index writer object.
            //Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();
            //Lucene.Net.Documents.Field fldText = new Lucene.Net.Documents.Field("text", System.IO.File.ReadAllText(@"C:\Users\zdanc\Desktop\test.txt"), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES);
            //doc.Add(fldText);
            //write the document to the index
            //idxw.AddDocument(doc);
            //optimize and close the writer
            //idxw.Optimize();
            //idxw.Close();
            //Response.Write("Indexing Done");
            //var people = from m in _context.Persons
            //             select m;


            if (!String.IsNullOrEmpty(searchString))
            {
                var x = new LuceneService();
                x.BuildIndex(lista);
                var result = x.Search(searchString);
                
                return View(result);
            }
            return View(db.Characters.ToList());
        }

        // GET: Characters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // GET: Characters/Create
        public async System.Threading.Tasks.Task<ActionResult> Create()
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var page = await browser.NewPageAsync();
            await page.GoToAsync("http:/www.google.com");
            await page.PdfAsync(@"C:\Users\zdanc\Desktop\PDF");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CharacterID,Name")] Character character)
        {
            if (ModelState.IsValid)
            {
                db.Characters.Add(character);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(character);
        }

        // GET: Characters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CharacterID,Name")] Character character)
        {
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(character);
        }

        // GET: Characters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
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
