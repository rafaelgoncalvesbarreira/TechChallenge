using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infra.Contexts
{
    public class TechChallengeDbContext : DbContext
    {
        public TechChallengeDbContext() : base()
        {

        }

        public TechChallengeDbContext(DbContextOptions<TechChallengeDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<CreditCard> CreditCards { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id =1,
                    Name = "Customer 1"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Customer 2"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Customer 3"
                }
            );
        }
    }
}
