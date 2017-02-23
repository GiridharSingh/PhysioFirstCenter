using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interface;
using Models;
using System.IO;

namespace Admin.Controllers
{
    public class EmployeesController : Controller
    {
        private PhysioDevEntities db = new PhysioDevEntities();
        private IEmployee _IEmployee;
        public EmployeesController(IEmployee employee)
        {
            this._IEmployee = employee;
        }
        // GET: Employees
        public ActionResult Index()
        {
            return View(_IEmployee.GetEmployees());
        }

        // GET: Employees/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             var employee=_IEmployee.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.genderList = new SelectList(new List<string>() { "Male", "Female" });
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = " First_Name,Last_Name,Email,Password,Gender,Designation ,Description,Joined_date")] EmployeeModel employee, HttpPostedFileBase file)
        {
            int retvalue = _IEmployee.AddEmployee(employee, file);
            if (retvalue > 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.genderList = new SelectList(new List<string>() { "Male", "Female" });
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _IEmployee.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.genderList = new SelectList(new List<string>() { "Male", "Female" });
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpId,First_Name,Last_Name,Email,Gender,Designation ,Description,Joined_date ")] EmployeeModel employee, HttpPostedFileBase file)
        {
            if (file != null)
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                employee.Image = target.ToArray();
            }
            else
            {
                var oldemp = db.Employees.Where(m => m.EmpId == employee.EmpId).FirstOrDefault();
                employee.Image = oldemp.Image;
            }

            int retvalue = _IEmployee.UpdateEmployee(employee);
            if (retvalue > 0)
            {

                return RedirectToAction("Index");
            }
            ViewBag.genderList = new SelectList(new List<string>() { "Male", "Female" });
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _IEmployee.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _IEmployee.RemoveEmoployee(id);
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
