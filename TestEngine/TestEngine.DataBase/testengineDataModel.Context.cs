﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestEngine.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Demo_sukhvirEntities : DbContext
    {
        public Demo_sukhvirEntities()
            : base("name=Demo_sukhvirEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<customexam> customexams { get; set; }
        public virtual DbSet<Customtopic> Customtopics { get; set; }
        public virtual DbSet<CustomSection> CustomSections { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
    }
}
