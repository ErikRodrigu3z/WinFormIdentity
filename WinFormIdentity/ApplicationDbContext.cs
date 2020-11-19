using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WinFormIdentity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

/*
 * nuggets necesarios
 Microsoft.EntityFrameworkCore.SqlServer
 Microsoft.Extensions.DependencyInjection;
 Microsoft.Extensions.Hosting; 
 FontAwesome.Sharp.Pro
 Microsoft.AspNetCore.Identity.EntityFrameworkCore

 */


namespace WinFormIdentity
{
    public class ApplicationDbContext : IdentityDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            string conn = builder.Build().GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(conn)
            .EnableSensitiveDataLogging(true);
        }

        //Fluent API - conventions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }

        public DbSet<AppUsers> AppUsers { get; set; }


    }
}
