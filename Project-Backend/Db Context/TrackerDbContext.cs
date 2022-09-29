////using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Project_Backend.Models;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;

//namespace Project_Backend.Db_Context
//{
//    public class TrackerDbContext : DbContext
//    {
//        public TrackerDbContext() : base("name=TrackerDbContext")
//        {
//        }

//        //DB set
//        public DbSet<Registration> Registration { get; set; }
//        public DbSet<Orders> Orders { get; set; }
//        public DbSet<Vendors> Vendors { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {

//            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


//        }
//    }

//}
