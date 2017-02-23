//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public long AppointmentId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> AppointmentDate { get; set; }
        public Nullable<long> ServiceId { get; set; }
        public Nullable<int> TockenNo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> AssignedTo { get; set; }
        public Nullable<long> ProceedBy { get; set; }
        public Nullable<bool> IsProceed { get; set; }
    
        public virtual Service Service { get; set; }
    }
}
