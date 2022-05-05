using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace DatabaseLayer.Data
{
    public class DatabaseContext : DbContext
    {
        private static string ConnectionString =
            $@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=CustomerDB;AttachDBFilename={Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CustomerDB.mdf")}";

        public DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ?????
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer(CreationConnectionString);
            //} 
            
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public static void SeedData()
        {
            using var context = new DatabaseContext();
            //context.GetService<IMigrator>().Migrate("Initial");
            context.Database.Migrate();
            if (!context.Customer.Any())
            {
                context.AddRange(Enumerable.Range(1, 50).Select(i => new Customer { FirstName = "Petro", LastName = $"Piatochkin {i}" }));
            }
            context.SaveChanges();
        }
    }
}
