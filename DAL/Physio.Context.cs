﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PhysioDevEntities : DbContext
    {
        public PhysioDevEntities()
            : base("name=PhysioDevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<AboutClinic> AboutClinics { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
    }
}
