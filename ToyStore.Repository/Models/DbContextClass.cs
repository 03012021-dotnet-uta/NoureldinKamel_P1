using System;
using Microsoft.EntityFrameworkCore;
using ToyStore.Models.DBModels;
using ToyStore.Models.Abstracts;
using Microsoft.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ToyStore.Repository.Models
{
    //todo: change protection level
    public class DbContextClass : DbContext
    {
        public DbContextClass() : base()
        {

        }
        public DbContextClass(DbContextOptions<DbContextClass> options) : base()
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sellable> Sellables { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        // public DbSet<SellableTag> SellableTags { get; set; }
        public DbSet<SellableStack> SellableStacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=.\PRODDB;Database=ToyStoreP1NNK;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=ToyStoreP1NNK;User Id=SA;Password=1Secure*Password1;");
            // optionsBuilder.UseSqlServer(Microsoft.Extensions.Configuration.IConfiguration.GetConnectionString("ToysDb"));
            // base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.Entity<SellableStack>().HasData(
            //     s1, s2, s3, s4);

            modelBuilder.Entity<Tag>().HasIndex(u => u.TagName).IsUnique();
            // modelBuilder.Entity<School>().HasMany(s => s.Students).WithOne(s => s.School);
            // people : student => school
            // sellable : offer => sellable
            modelBuilder.Entity<Sellable>().HasMany(s => s.Products).WithOne(s => s.CurrentOffer);
            // modelBuilder.Entity<SellableTag>().HasKey(st => new { st.TagType.TagId, st.SellableItem.SellableId });

            base.OnModelCreating(modelBuilder);
        }
    }
}