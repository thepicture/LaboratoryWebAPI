﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LaboratoryWebAPI.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LaboratoryDatabaseEntities : DbContext
    {
        public LaboratoryDatabaseEntities()
            : base("name=LaboratoryDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Analyzer> Analyzer { get; set; }
        public virtual DbSet<AppliedService> AppliedService { get; set; }
        public virtual DbSet<BarcodeOfPatient> BarcodeOfPatient { get; set; }
        public virtual DbSet<HistoryOfLogin> HistoryOfLogin { get; set; }
        public virtual DbSet<InsuranceCompany> InsuranceCompany { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<StatusOfAppliedService> StatusOfAppliedService { get; set; }
        public virtual DbSet<StatusOfOrder> StatusOfOrder { get; set; }
        public virtual DbSet<TypeOfInsurancePolicy> TypeOfInsurancePolicy { get; set; }
        public virtual DbSet<TypeOfUser> TypeOfUser { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<LaboratoryNews> LaboratoryNews { get; set; }
    }
}
