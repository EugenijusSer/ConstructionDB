﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConstructionDataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ConstructionDBEntities : DbContext
    {
        public ConstructionDBEntities()
            : base("name=ConstructionDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Darbuotojas> Darbuotojai { get; set; }
        public DbSet<Imone> Imones { get; set; }
        public DbSet<Priziuretojas> Priziuretojai { get; set; }
        public DbSet<Statybininkas> Statybininkai { get; set; }
        public DbSet<Statybviete> Statybvietes { get; set; }
    }
}