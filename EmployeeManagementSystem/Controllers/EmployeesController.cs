using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        ProjectEMSEntities1 database = new ProjectEMSEntities1();
        // GET: Employees
        public ActionResult AddEmp()
        {

            return View();
        }
        
        

        // GET: Employees/Details/5
        public ActionResult Empdetails(int id)

        {

            var data = database.t_Employees.FirstOrDefault(x => x.Employee_ID == id);
            ViewBag.Employeedata = data;
            //ViewData["Employeedata"] = data;
            return View();
        }
      
        public ActionResult Employees( )
        {
        
            t_Employees empdata = new t_Employees();
            ProjectEMSEntities1 database = new ProjectEMSEntities1();
            var departmentlist = database.t_Departments.ToList();
            SelectList list = new SelectList(departmentlist, "Id", "Department");

            var data = from employees in database.t_Employees select employees;
            ViewBag.DepartmentList = departmentlist;
            ViewBag.Employee = data;
            //database.t_Employees.ToList()
            return View();

            //return View(empdata);
        }

        // GET: Employees/Create
        [HttpPost]
        public ActionResult Employees(EmployeesModel AddEmployee, Mailer mailer)
        {

            // TODO: Add insert logic here
         
            t_Employees EmployeesData = new t_Employees();
           
            //EmployeesData.Employee_ID = 1;
            if (ModelState.IsValid)
            {
                using (ProjectEMSEntities1 database = new ProjectEMSEntities1())
                {
                    
                    var email = database.t_Employees.FirstOrDefault(u => u.Email.ToLower() == AddEmployee.Email.ToLower());
                    var username = database.t_Employees.FirstOrDefault(u => u.Username == AddEmployee.Username);
                    try
                    {
                        // Check if email already exists
                        if (username != null)
                        {
                            ModelState.AddModelError("UserName", "Username already exists");

                        }
                        else if (email != null)
                        {
                            ModelState.AddModelError("Email", "Email address already exists. Enter different email address.");
                        }

                        else
                        {
                            EmployeesData.RefHRID = Convert.ToInt32(Session["HRID"]);
                            EmployeesData.Firstname = AddEmployee.Firstname;
                            EmployeesData.Lastname = AddEmployee.Lastname;
                            EmployeesData.Username = AddEmployee.Username;
                            EmployeesData.Email = AddEmployee.Email;
                            EmployeesData.Mobileno = AddEmployee.Mobileno;
                            string dateString = AddEmployee.JoiningDate;
                            EmployeesData.JoiningDate = DateTime.Parse(dateString);
                            EmployeesData.Department = AddEmployee.Department;
                            EmployeesData.Designation = AddEmployee.Designation;
                            EmployeesData.Password = AddEmployee.Password;
                            database.t_Employees.Add(EmployeesData);
                            database.SaveChanges();
                            mailer.To = AddEmployee.Email;
                            mailer.Body = $"Your ID is: 10 \n Password is: {AddEmployee.Password} ";
                            mailer.SendMail();

                        }
                    }
                    catch (MembershipCreateUserException e)
                    {

                        ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                    }
                }
                         

               // return RedirectToAction("Employees", "Employees");
            }
            return RedirectToAction("Employees", "Employees");
            //return View();


            //EmployeesData.JoiningDate = Convert.ToDateTime(AddEmployee.JoiningDate);

            //EmployeesData.RefHRID = int.Parse(Session["HRID"]);



        }

        private Exception ErrorCodeToString(MembershipCreateStatus statusCode)
        {
            throw new NotImplementedException();
        }

        // POST: Employees/Create
        //FormCollection collection

        public ActionResult Create()
        {
            return View();
        }

        // GET: Employees/Edit/5
        public ActionResult Editdata(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_Employees t_Employees = database.t_Employees.Find(id);
            if (t_Employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefHRID = new SelectList(database.HR_SignUp, "id", "username", t_Employees.RefHRID);
            return View(t_Employees);
            /*
            var data = database.t_Employees.FirstOrDefault(x => x.Employee_ID == id);
            ViewBag.Employeedata = data;
        
            return View();*/
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Editdata(int id, t_Employees AddEmployee)
        {
            t_Employees empdata = new t_Employees();
            
                using (ProjectEMSEntities1 database = new ProjectEMSEntities1())
                {
                    var email = database.t_Employees.FirstOrDefault(u => u.Email.ToLower() == AddEmployee.Email.ToLower());
                    var username = database.t_Employees.FirstOrDefault(u => u.Username == AddEmployee.Username);
                    try
                    {
                        // Check if email already exists
                   

                            var EmployeesData = database.t_Employees.Single(x => x.Employee_ID == id);

                            //harvest the values from the student object in the StudentFromDB object manually  
                           // EmployeesData.RefHRID = Convert.ToInt32(Session["HRID"]);
                            EmployeesData.Firstname = AddEmployee.Firstname;
                            EmployeesData.Lastname = AddEmployee.Lastname;
                            EmployeesData.Username = AddEmployee.Username;
                            EmployeesData.Email = AddEmployee.Email;
                            EmployeesData.Mobileno = AddEmployee.Mobileno;
                    //    string dateString = AddEmployee.JoiningDate;
                            EmployeesData.JoiningDate = AddEmployee.JoiningDate;
                            EmployeesData.Department = AddEmployee.Department;
                            EmployeesData.Designation = AddEmployee.Designation;
                            EmployeesData.Password = AddEmployee.Password;
                            database.t_Employees.Add(EmployeesData);
                           // studentFromDB.StudentGender = student.StudentGender;
                           // studentFromDB.Course_Id = student.Course_Id;
                            //retrieve the StudentName from the database and assign it to the student object StudentName  
                            //student.StudentName = studentFromDB.StudentName;

                            if (ModelState.IsValid)
                            {
                                database.Entry(EmployeesData).State = EntityState.Modified;// pass student entity as StudentFromDB  
                                database.SaveChanges();
                                return RedirectToAction("Employees","Employees");
                            }


                        
                    }
                    catch (MembershipCreateUserException e)
                    {

                        ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                    }
                }


                //return RedirectToAction("Employees", "Employees");
            


            return View();
            /*
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }*/
        }

        // GET: Employees/Delete/5
        /*
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
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
        }*/

        //Search bar
        [HttpPost]
        public JsonResult SearchCustomers(string customerName)
        {

            //var empdetails = from c in database.t_Employees
            //              where c.Firstname.Contains(customerName)
            //            select c;

            List<t_Employees> empdetails = new List<t_Employees>();
            try
            {
               
                var id = Convert.ToInt32(customerName);
                //var empdetails = database.t_Employees.Where(x => x.Firstname.Contains(customerName));
                empdetails = database.t_Employees.Where(x => x.Employee_ID == id).ToList();
                //t_Employees empdetails = database.t_Employees.Find(id);


                
            }
            catch (FormatException)
            {

            }
            return Json(empdetails);

            //  return Json(empdetails);





            //t_Employees empdetails = new t_Employees
            //{
            //    Employee_ID = 101,
            //    Firstname = "Bouchers",
            //    Lastname = "The basic stocks"
            //};





        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int Employee_ID = Convert.ToInt32(id);
            t_Employees t_Employees = database.t_Employees.Find(Employee_ID);
            database.t_Employees.Remove(t_Employees);
            database.SaveChanges();
            return RedirectToAction("Employees");
             
        }
        [HttpPost]
        public JsonResult GetSearchData(string SearchBy, string SearchValue)
        {
            string sea = SearchValue;
            List<t_Employees> empdetails = new List<t_Employees>();
            if(SearchBy == "Id")
            {
                try
                {
                    int Id = Convert.ToInt32(SearchValue);
                    empdetails = database.t_Employees.Where(x => x.Employee_ID == Id || SearchValue == null).ToList();

                }
                catch (FormatException)
                {
                    Console.WriteLine("(0) is not id", SearchValue);
                }
                return Json(empdetails, JsonRequestBehavior.AllowGet);

            }
            else
            {
                empdetails = database.t_Employees.Where(x => x.Firstname.Contains(SearchValue) || SearchValue == null).ToList();
                return Json(empdetails, JsonRequestBehavior.AllowGet);

            }

        }
    }
}

//[DataType(DataType.Date)]
//[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] 