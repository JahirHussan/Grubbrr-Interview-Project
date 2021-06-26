
using Grubbrr_Interview_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace GRUBBRR.Context
{
    public class InvoiceContext:DbContext
    {
        public InvoiceContext():base("InvoiceContextDB")
        {
            try
            {
                Database.SetInitializer<InvoiceContext>(new DropCreateDatabaseIfModelChanges<InvoiceContext>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Status> InvoiceStatuses { get; set; }
        public DbSet<InvoiceInfo> InvoiceInfos { get; set; }
        public DbSet<InvoiceList> InvoiceList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}