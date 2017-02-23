using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
   public interface IAppointment
    {
        int CreateAppointment(AppointmentModel objappointment);
        int FixAppointment(AppointmentModel objappointment);
        int ProcessAppointment(long appointmentId);
        AppointmentModel GetAppointment(long? appointmentId);
        List<AppointmentModel> GetAllAppointment();
    }
}
