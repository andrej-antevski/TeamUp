using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using TeamUp.Models;


namespace TeamUp.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teams
        public ActionResult Index()
        {
            ViewBag.IdLoggedIn = db.Users.Find(User.Identity.GetUserId()).Id;
            return View(db.Teams.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            ViewBag.IdLoggedIn = userLoggedIn.Id;
            var mem = false;
            foreach(var member in team.Members)
            {
                if (member.Id == userLoggedIn.Id)
                {
                    mem = true;
                    break;
                }
            }

            if (!mem)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,DueDate")] Team team)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                team.Admin = user;
                team.Members.Add(user);
                db.Teams.Add(team);
                user.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (team.Admin.Id != userLoggedIn.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,DueDate")] Team team)
        {
            if (ModelState.IsValid)
            {
                
                var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
                var toChange = db.Teams.Find(team.Id);
                if (toChange.Admin.Id != userLoggedIn.Id)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                toChange.Name = team.Name;
                toChange.Description = team.Description;
                toChange.DueDate = team.DueDate;
                db.SaveChanges();
                return RedirectToAction("Details/" + team.Id);
            }

            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (team.Admin.Id != userLoggedIn.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (team.Admin.Id != userLoggedIn.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team.Admin = null;
            foreach (var msg in team.Chat.ToList())
            {
                db.Messages.Remove(msg);
            }
            foreach (var role in team.RolesNeeded.ToList())
            {
                role.FilledBy = null;
                db.RolesNeeded.Remove(role);
            }
            foreach(var member in team.Members.ToList())
            {
                member.Teams.Remove(team);
            }
            foreach(var application in db.Applications.ToList())
            {
                if (application.To.Id == team.Id)
                {
                    db.Applications.Remove(application);
                }
            }
            team.Members = null;
            db.SaveChanges();
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddNewRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (team.Admin.Id != userLoggedIn.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new AddTeamRole();
            model.TeamId = (int)id;

            return View(model);
        }
        [HttpPost]
        public ActionResult AddNewRole(AddTeamRole model)
        {
            var Team = db.Teams.Find(model.TeamId);
            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (Team.Admin.Id != userLoggedIn.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.RolesNeeded.Filled = false;
            model.RolesNeeded.FilledBy = null;
            Team.RolesNeeded.Add(model.RolesNeeded);
            db.SaveChanges();
            return RedirectToAction("Details/" + model.TeamId);
        }
        public void DeleteRole(int id)
        {
            db.RolesNeeded.Remove(db.RolesNeeded.Find(id));
            db.SaveChanges();
        }
        public void AddMessage(int id, string message)
        {
            var team = db.Teams.Find(id);

            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            var canPost = false;
            foreach (var member in team.Members)
            {
                if (member.Id == userLoggedIn.Id)
                {
                    canPost = true;
                    break;
                }   
            }
            if (!canPost)
            {
                return;
            }

            Message msg = new Message();
            msg.From = db.Users.Find(User.Identity.GetUserId());
            msg.Text = message;
            msg.TimeSent = DateTime.Now;
            team.Chat.Add(msg);
            db.SaveChanges();

        }
        public void Apply(int id, string roleId, string message)
        {
            var team = db.Teams.Find(id);
            var role_id = int.Parse(roleId);
            var r = new RolesNeeded();
            foreach(var role in team.RolesNeeded)
            {
                if (role.Id == role_id)
                {
                    r = role;
                }
            }
            var from = db.Users.Find(User.Identity.GetUserId());
            var application = new Application();
            application.ForRole = r;
            application.From = from;
            application.Description = message;
            application.DateSent = DateTime.Now;
            application.Status = "w";
            application.To = team;
            db.Applications.Add(application);
            from.Applications.Add(application);
            db.SaveChanges();
        }
        public ActionResult Applications(int id)
        {

            var team = db.Teams.Find(id);

            if (User.Identity.GetUserId() != team.Admin.Id)
            {
                return RedirectToAction("Index", db.Teams.ToList());
            }

                List<Application> applications = new List<Application>();
            foreach(var app in db.Applications.ToList())
            {
                if (app.To.Id == id && app.Status == "w")
                {
                    applications.Add(app);
                }
            }

            return View(applications);
        }

        public ActionResult AcceptApplication(int id)
        {

            var app = db.Applications.Find(id);
            var user = db.Users.Find(app.From.Id);
            var team = app.To;

            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (team.Admin.Id != userLoggedIn.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = app.ForRole;
            
            var roleInTeam = team.RolesNeeded.FirstOrDefault(m => m.Id == role.Id);
            app.Status = "a";
            roleInTeam.Filled = true;
            roleInTeam.FilledBy = user;
            foreach (var application in db.Applications.ToList())
            {
                if (application.To.Id == team.Id && application.Status == "w" && application.ForRole.Id == roleInTeam.Id)
                {
                    application.Status = "d";
                }
            }
            team.Members.Add(user);
            user.Teams.Add(team);
            db.SaveChanges();
            return RedirectToAction("Applications/" + team.Id);
        }
        public void RemoveUser(int id, string roleId)
        {
            var team = db.Teams.Find(id);
            var user = db.RolesNeeded.Find(int.Parse(roleId)).FilledBy;

            team.Members.Remove(user);
            user.Teams.Remove(team);
            foreach (var role in team.RolesNeeded)
            {
                if (role.FilledBy.Id == user.Id)
                {
                    role.FilledBy = null;
                    role.Filled = false;
                }
            }
            db.SaveChanges();
        }
        public ActionResult DenyApplication(int id)
        {
            var app = db.Applications.Find(id);
            var team = app.To;
            var userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (team.Admin.Id != userLoggedIn.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            app.Status = "d";
            db.SaveChanges();
            return View();
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
