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
        //private readonly List<TestimonialsModel> _Testimonials = new List<TestimonialsModel>();
        public AboutViewModel()
        {
            HistoryDAL objHistoryDAL = new HistoryDAL();
            EmployeeDAL objEmployeeDAL = new EmployeeDAL();
            TestimonialsDAL objTestimonialsDAL = new TestimonialsDAL();
            _Histories = objHistoryDAL.GetAllHistories();
            _Employees = objEmployeeDAL.GetEmployees();
           // _Testimonials = objTestimonialsDAL.GetAllTestimonial();
        }
        public List<HistoryModel> lstHistory
        {
            get { return _Histories; }

        }
        public List<EmployeeModel> lstEmployee
        {
            get { return _Employees; }
        }
        //public List<TestimonialsModel> lstTestimonial
        //{
        //    get { return _Testimonials; }
        //}
    }
}
