using System;
using Microsoft.EntityFrameworkCore;
using ApiCrud.Models;
using ApiCrud.Data.Maps;

namespace ApiCrud.Data
{
    public class StoreDataContext : DbContext
    {
        


        public StoreDataContext(DbContextOptions<StoreDataContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMaps());
        }
    }
}