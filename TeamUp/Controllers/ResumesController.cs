using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamUp.Models;

namespace TeamUp.Controllers
{
    public class ResumesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Resumes
        public ActionResult Index()
        {
            return View(db.Resumes.ToList());
        }

        // GET: Resumes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }

        // GET: Resumes/Create
        public ActionResult Create()
        {
            ViewBag.Links = new List<Link>();
            return View();
        }

        // POST: Resumes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Resume resume)
        {
            if (ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                db.Resumes.Add(resume);
                db.Users.Find(userid).Resume = resume;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resume);
        }

        // GET: Resumes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }

        // POST: Resumes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Education,Experience,HobbiesInterests")] Resume resume)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resume).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resume);
        }

        // GET: Resumes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }

        // POST: Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resume resume = db.Resumes.Find(id);
            db.Resumes.Remove(resume);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddLinks(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            var model = new ResumeAddFields();
            model.ResumeId = (int)id;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddLinks(ResumeAddFields model)
        {
            var resume = db.Resumes.Find(model.ResumeId);
            resume.Links.Add(model.Link);
            db.SaveChanges();
            return RedirectToAction("Details/" + model.ResumeId);
        }
        public ActionResult AddTech(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            var model = new ResumeAddTech();
            model.ResumeId = (int)id;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddTech(ResumeAddTech model)
        {
            var resume = db.Resumes.Find(model.ResumeId);
            resume.Technologies.Add(model.Technology);
            db.SaveChanges();
            return RedirectToAction("Details/" + model.ResumeId);
        }
        public void DeleteTech(int? id)
        {
            if (id == null)
            {
                return;
            }
            var techItem = db.Technologies.Remove(db.Technologies.Find(id));
            db.SaveChanges();
            
        }
        public void DeleteLink(int? id)
        {
            if (id == null)
            {
                return;
            }
            var techItem = db.Links.Remove(db.Links.Find(id));
            db.SaveChanges();
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
