using Microsoft.EntityFrameworkCore;
using Project_Backend.Models;
using System.Numerics;

namespace Project_Backend.Db_Context
{
    public class VendorDbContext : DbContext
    {
        public VendorDbContext(DbContextOptions<VendorDbContext> options) : base(options)
        {
        }

        public DbSet<Vendors> Vendors { get; set; }
    }
}
