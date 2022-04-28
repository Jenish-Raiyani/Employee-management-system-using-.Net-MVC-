using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeManagementSystem.Controllers
{
    public class AccountsController : Controller
    {
        ProjectEMSEntities1 database = new ProjectEMSEntities1();
        // GET: Accounts
        public ActionResult HRSignup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HRSignup(Accountsmodel reg)
        {
            if (ModelState.IsValid)
            {
                using (ProjectEMSEntities1 database = new ProjectEMSEntities1())
                {
                    var email = database.HR_SignUp.FirstOrDefault(u => u.email.ToLower() == reg.email.ToLower());
                    var username = database.HR_SignUp.FirstOrDefault(u => u.username == reg.username);
                    try
                    {
                        // Check if email already exists
                        if (username != null)
                        {
                            ModelState.AddModelError("UserName", "Username already exists");

                        }
                        else if (email != null )
                        {
                            ModelState.AddModelError("Email", "Email address already exists. Enter different email address.");
                        }
                       
                        else
                        {
                            HR_SignUp HRdata = new HR_SignUp();
                            HRdata.id = 1;
                            HRdata.username = reg.username;
                            HRdata.email = reg.email;
                            HRdata.mobileno = reg.mobileno;
                            string pass = reg.password;
                            //Hasher.Encrypt(pass)
                            HRdata.password = pass;
                            database.HR_SignUp.Add(HRdata);
                            database.SaveChanges();
                            return RedirectToAction("Login", "Accounts");
                        }
                       
                    }
                    catch (MembershipCreateUserException e)
                    {

                        ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                    }
                    //    HR_SignUp HRdata = new HR_SignUp();
                }
            }
            
            
            return View();
        }
private Exception ErrorCodeToString(MembershipCreateStatus statusCode)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Login()
        {
           
            return View(); 
        }
        [HttpPost]
        public ActionResult Login(Accountsmodel log)
        {
            // string password = Hasher.Decrypt(log.password);
            HR_SignUp HRdata = new HR_SignUp();
            //  string pass = HRdata.password.FirstOrDefault();
            //Hasher.Decrypt(model.password)
            //var credentials = database.HR_SignUp.Where(x => x.email == log.email && Hasher.Decrypt(x.password) == log.password );
            var credentials = database.HR_SignUp.Where(query => query.email.Equals(log.email) 
            && query.password.Equals(log.password)).FirstOrDefault();
            //            var credentials = database.HR_SignUp.Where(model => model.email == log.email && Hasher.Decrypt(model.password) == log.password);
            if (credentials == null)
                {
                    ViewBag.ErrorMessage = "Login Failed";
                    

                }
            else

                {
                //int sessionid = credentials.emai;
                //Session["HRid"] = sessionid;
                Session["HRID"] = credentials.id.ToString();
                Session["Username"] = credentials.username.ToString();
                return RedirectToAction("Index", "Dashboard");
                }
            return View();

             
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["HRID"] = null;
            Session["Username"] = null;
            return RedirectToAction("Index","Home");
        }
        // GET: Accounts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Accounts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}


