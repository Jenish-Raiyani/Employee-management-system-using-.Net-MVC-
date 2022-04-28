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
    public class PayrollController : Controller
    {
        private ProjectEMSEntities1 db = new ProjectEMSEntities1();

        // GET: Payroll
        public ActionResult Index()
        {
            var data = db.t_payroll.ToList();
            return View(db.t_payroll.ToList());
        }
        public ActionResult Payslip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_payroll t_payroll = db.t_payroll.Find(id);
            if (t_payroll == null)
            {
                return HttpNotFound();
            }
            return View(t_payroll);
        }

        // GET: Payroll/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_payroll t_payroll = db.t_payroll.Find(id);
            if (t_payroll == null)
            {
                return HttpNotFound();
            }
            return View(t_payroll);
        }

        // GET: Payroll/Create
        public ActionResult Create()
        {
            ProjectEMSEntities1 database = new ProjectEMSEntities1();
            var Employeelist = database.t_Employees.ToList();
            SelectList list = new SelectList(Employeelist, "Employee_ID", "Firstname");
           
            var items = db.t_Employees.ToList();
            if (items != null)
            {
                ViewBag.Employeelist = items;
            }

            return View();
        }

        // POST: Payroll/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,Employee_name,Employee_Id,NetSalary,E_Basic,E_DA,E_HRA,E_Conveyance,Total_Earnings,E_Allowance,D_TDS,D_ESI,D_PF,Tax,Total_Deductions")] t_payroll t_payroll)
        {
            if (ModelState.IsValid)
            {
                db.t_payroll.Add(t_payroll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_payroll);
        }

        // GET: Payroll/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_payroll t_payroll = db.t_payroll.Find(id);
            if (t_payroll == null)
            {
                return HttpNotFound();
            }
            return View(t_payroll);
        }

        // POST: Payroll/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,Employee_name,Employee_Id,NetSalary,E_Basic,E_DA,E_HRA,E_Conveyance,Total_Earnings,E_Allowance,D_TDS,D_ESI,D_PF,Tax,Total_Deductions")] t_payroll t_payroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_payroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_payroll);
        }

        // GET: Payroll/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_payroll t_payroll = db.t_payroll.Find(id);
            if (t_payroll == null)
            {
                return HttpNotFound();
            }
            return View(t_payroll);
        }

        // POST: Payroll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            t_payroll t_payroll = db.t_payroll.Find(id);
            db.t_payroll.Remove(t_payroll);
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
