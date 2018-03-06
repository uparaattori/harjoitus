using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using harjoitus.model;
using MySql.Data;
using System;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace harjoitus.Database
{
    public class SightingContext: DbContext 
    {
        public SightingContext(): base()
        {
            
        }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseMySQL("Database=test;Data Source=localhost;User Id=root;Password=;Old Guids=True;");
           optionsBuilder.UseMySQL(Constants.ConnectionString);
        }
        public DbSet<Bird> birds { get; set; }
        public DbSet<Sighting> sightings { get; set; }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sighting>()
                .HasOne(p => p.Bird)
                .WithMany(b => b.Sightings)
                .HasForeignKey(p => p.BirdID);
        }
    }
    
     
}