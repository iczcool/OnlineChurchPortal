using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineWebPortal.Models;
using Microsoft.AspNetCore.Identity;

namespace OnlineWebPortal.Data
{
    public class OnlineWebPortalDbContext : DbContext
    {
        public OnlineWebPortalDbContext()
        {
        }

        public OnlineWebPortalDbContext(DbContextOptions<OnlineWebPortalDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<ChurchGroup> ChurchGroups  { get; set; }
        public DbSet<Enquiry> Enquiries  { get; set; }
        public DbSet<RegUserChurchGroup> RegUserChurchGroups  { get; set; }
        public DbSet<Payment> Payments  { get; set; }
        public DbSet<RegUser> RegUsers { get; set; }
        public DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:onlinewebportaldbserver.database.windows.net,1433;Initial Catalog=OnlineWebPortal_db;Persist Security Info=False;User ID=iczcool;Password=Goldbook497#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlineWebPortal;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RegUserChurchGroup>()
                .HasKey(rc => new { rc.RegUserID, rc.ChurchGroupID });
            modelBuilder.Entity<RegUserChurchGroup>()
                .HasOne(rc => rc.RegUsers)
                .WithMany(j => j.RegUserChurchGroups)
                .HasForeignKey(rc => rc.RegUserID);
            modelBuilder.Entity<RegUserChurchGroup>()
                .HasOne(rc => rc.ChurchGroups)
                .WithMany(k => k.RegUserChurchGroups)
                .HasForeignKey(rc => rc.ChurchGroupID);
        }
    }
}
