using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using database.Models;

namespace database.Controllers
{
    public class StudentFormsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: StudentForms
        public ActionResult Index()
        {
            var studentForms = db.StudentForms.Include(s => s.Nationality).Include(s => s.Religion);
            return View(studentForms.ToList());
        }

        // GET: StudentForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentForm studentForm = db.StudentForms.Find(id);
            if (studentForm == null)
            {
                return HttpNotFound();
            }
            return View(studentForm);
        }

        // GET: StudentForms/Create
        public ActionResult Create()
        {
            ViewBag.Nation = new SelectList(db.Nationalities, "Id", "Nationality1");
            ViewBag.ReligionName = new SelectList(db.Religions, "Id", "ReligionName");
            return View();
        }

        // POST: StudentForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,FName,Nation,ReligionName,DOB")] StudentForm studentForm)
        {
            if (ModelState.IsValid)
            {
                db.StudentForms.Add(studentForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Nation = new SelectList(db.Nationalities, "Id", "Nationality1", studentForm.Nation);
            ViewBag.ReligionName = new SelectList(db.Religions, "Id", "ReligionName", studentForm.ReligionName);
            return View(studentForm);
        }

        // GET: StudentForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentForm studentForm = db.StudentForms.Find(id);
            if (studentForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nation = new SelectList(db.Nationalities, "Id", "Nationality1", studentForm.Nation);
            ViewBag.ReligionName = new SelectList(db.Religions, "Id", "ReligionName", studentForm.ReligionName);
            return View(studentForm);
        }

        // POST: StudentForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FName,Nation,ReligionName,DOB")] StudentForm studentForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Nation = new SelectList(db.Nationalities, "Id", "Nationality1", studentForm.Nation);
            ViewBag.ReligionName = new SelectList(db.Religions, "Id", "ReligionName", studentForm.ReligionName);
            return View(studentForm);
        }

        // GET: StudentForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentForm studentForm = db.StudentForms.Find(id);
            if (studentForm == null)
            {
                return HttpNotFound();
            }
            return View(studentForm);
        }

        // POST: StudentForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentForm studentForm = db.StudentForms.Find(id);
            db.StudentForms.Remove(studentForm);
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
