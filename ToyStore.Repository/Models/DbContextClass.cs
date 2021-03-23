using System;
using Microsoft.EntityFrameworkCore;
using ToyStore.Models.DBModels;
using ToyStore.Models.Interfaces;

namespace PizzaBox.Domain.Models
{
    //todo: change protection level
    internal class DbContextClass : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sellable> Sellables { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=.\PRODDB;Database=ToyStoreP1NNK;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=ToyStoreP1NNK;User Id=SA;Password=1Secure*Password1;");
            // base.OnConfiguring(optionsBuilder);
        }
    }
}