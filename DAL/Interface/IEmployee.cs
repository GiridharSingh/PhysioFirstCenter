using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.Interface
{
  public  interface IEmployee
    {
        int AddEmployee(EmployeeModel employee, HttpPostedFileBase file);
        int UpdateEmployee(EmployeeModel employee);
        EmployeeModel GetEmployee(long? empId);
        List<EmployeeModel> GetEmployees();
        void RemoveEmoployee(long empId);
    }
}
