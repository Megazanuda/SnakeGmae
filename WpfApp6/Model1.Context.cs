﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp6
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SankeEntities : DbContext
    {
        private static SankeEntities _context;
        public SankeEntities()
            : base("name=SankeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        public static SankeEntities GetContext()
        {
            if (_context == null)
                _context = new SankeEntities();
            return _context;
        }

        public virtual DbSet<TableScore1> TableScore1 { get; set; }
    }
}