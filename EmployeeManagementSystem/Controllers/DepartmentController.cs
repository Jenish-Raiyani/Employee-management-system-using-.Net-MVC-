using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementSystem;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private ProjectEMSEntities1 db = new ProjectEMSEntities1();

        // GET: Department
        public ActionResult Index()
        {
            var t_Departments = db.t_Departments.Include(t => t.HR_SignUp);
            return View(t_Departments.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Departments t_Departments = db.t_Departments.Find(id);
            if (t_Departments == null)
            {
                return HttpNotFound();
            }
            return View(t_Departments);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.RefHRID = new SelectList(db.HR_SignUp, "id", "username");
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HOD,RefHRID,Department")] t_Departments t_Departments)
        {
            if (ModelState.IsValid)
            {
                db.t_Departments.Add(t_Departments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefHRID = new SelectList(db.HR_SignUp, "id", "username", t_Departments.RefHRID);
            return View(t_Departments);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Departments t_Departments = db.t_Departments.Find(id);
            if (t_Departments == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefHRID = new SelectList(db.HR_SignUp, "id", "username", t_Departments.RefHRID);
            return View(t_Departments);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HOD,RefHRID,Department")] t_Departments t_Departments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Departments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefHRID = new SelectList(db.HR_SignUp, "id", "username", t_Departments.RefHRID);
            return View(t_Departments);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Departments t_Departments = db.t_Departments.Find(id);
            if (t_Departments == null)
            {
                return HttpNotFound();
            }
            return View(t_Departments);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            t_Departments t_Departments = db.t_Departments.Find(id);
            db.t_Departments.Remove(t_Departments);
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
