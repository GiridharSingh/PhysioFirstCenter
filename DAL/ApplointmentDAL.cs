using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class ApplointmentDAL : IAppointment
    {
        public int CreateAppointment(AppointmentModel objappointment)
        {
            throw new NotImplementedException();
        }

        public int FixAppointment(AppointmentModel objappointment)
        {
            throw new NotImplementedException();
        }

        public List<AppointmentModel> GetAllAppointment()
        {
            throw new NotImplementedException();
        }

        public AppointmentModel GetAppointment(long? appointmentId)
        {
            throw new NotImplementedException();
        }

        public int ProcessAppointment(long appointmentId)
        {
            throw new NotImplementedException();
        }
    }
}
