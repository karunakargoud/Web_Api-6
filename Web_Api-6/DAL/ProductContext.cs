using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web_Api_6.Models;

namespace Web_Api_6.DAL
{
    public class ProductContext:DbContext
    {
        public ProductContext()
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
        }
        public DbSet<Product> Products { get; set; }
    }
}