﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Casino_besit.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Casino_ProjectEntities : DbContext
    {
        public Casino_ProjectEntities()
            : base("name=Casino_ProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Game_History> Game_History { get; set; }
        public virtual DbSet<Game_Sessions> Game_Sessions { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}