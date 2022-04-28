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
    public class AdminManageLeavesController : Controller
    {
        private ProjectEMSEntities1 db = new ProjectEMSEntities1();

        // GET: AdminManageLeaves
        public ActionResult Index()
        {
            return View(db.t_leaves.ToList());
        }

        // GET: AdminManageLeaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_leaves t_leaves = db.t_leaves.Find(id);
            if (t_leaves == null)
            {
                return HttpNotFound();
            }
            return View(t_leaves);
        }

        // GET: AdminManageLeaves/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminManageLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Employee_ID,LeaveType,FromDate,ToDate,TotalDays,Reason,Status,ApprovedBy")] t_leaves t_leaves)
        {
            if (ModelState.IsValid)
            {
                t_leaves.ApprovedBy = "Jenish";
                db.t_leaves.Add(t_leaves);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_leaves);
        }

        // GET: AdminManageLeaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_leaves t_leaves = db.t_leaves.Find(id);
            if (t_leaves == null)
            {
                return HttpNotFound();
            }
            return View(t_leaves);
        }

        // POST: AdminManageLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Employee_ID,LeaveType,FromDate,ToDate,TotalDays,Reason,Status,ApprovedBy")] t_leaves t_leaves)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_leaves).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_leaves);
        }

        // GET: AdminManageLeaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_leaves t_leaves = db.t_leaves.Find(id);
            if (t_leaves == null)
            {
                return HttpNotFound();
            }
            return View(t_leaves);
        }

        // POST: AdminManageLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            t_leaves t_leaves = db.t_leaves.Find(id);
            db.t_leaves.Remove(t_leaves);
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
