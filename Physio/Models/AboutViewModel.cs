using System;
using System.Collections.Generic;
using Models;
using DAL;

namespace Physio.Models
{
  public  class AboutViewModel
    {
        
        private readonly List<HistoryModel> _Histories = new List<HistoryModel>();
        private readonly List<EmployeeModel> _Employees = new List<EmployeeModel>();        
        public AboutViewModel()
        {
            HistoryDAL objHistoryDAL = new HistoryDAL();
            EmployeeDAL objEmployeeDAL = new EmployeeDAL();          
            _Histories = objHistoryDAL.GetAllHistories();
            _Employees = objEmployeeDAL.GetEmployees();         
        }
        public List<HistoryModel> lstHistory
        {
            get { return _Histories; }

        }
        public List<EmployeeModel> lstEmployee
        {
            get { return _Employees; }
        }       
    }
}
