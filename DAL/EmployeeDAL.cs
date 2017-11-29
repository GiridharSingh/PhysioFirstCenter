using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;
using System.Web;
using System.Data.Entity;

namespace DAL
{
    public class EmployeeDAL : IEmployee
    {
        public int AddEmployee(EmployeeModel employee, HttpPostedFileBase file)
        {
           if(file!=null)
            {
                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                employee.Image = ms.ToArray();
            }
            employee.CreatedDate = DateTime.Now.Date;
            employee.UpdatedDate = DateTime.Now.Date;
            employee.IsActive = true;
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                try
                {
                    Employee emp = new Employee();
                    employee.CopyProperties(emp);
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public EmployeeModel GetEmployee(long? empId)
        {
            EmployeeModel objemp = new EmployeeModel();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var employee = db.Employees.Where(m => m.EmpId == empId).FirstOrDefault();
                employee.CopyProperties(objemp);
            }
            return objemp;
        }

        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> lstemp = new List<EmployeeModel>();
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var lstemployees = db.Employees.Where(m => m.IsActive == true).ToList();
                foreach (var employee in lstemployees)
                {
                    EmployeeModel objemp = new EmployeeModel();
                    employee.CopyProperties(objemp);
                    lstemp.Add(objemp);
                }
            }
            return lstemp;
        }

        public void RemoveEmoployee(long empId)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {
                var emp = db.Employees.Where(m => m.EmpId == empId).FirstOrDefault();
                emp.IsActive = false;
                db.SaveChanges();
            }
        }

        public int UpdateEmployee(EmployeeModel employee)
        {
            using (PhysioDevEntities db = new PhysioDevEntities())
            {

                employee.UpdatedDate = DateTime.Now.Date;

                try
                {
                    Employee objEmp = new Employee();
                    employee.CopyProperties(objEmp);
                    objEmp.IsActive = true;
                    db.Entry(objEmp).State = EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
    }
}
