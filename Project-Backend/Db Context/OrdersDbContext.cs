using Microsoft.EntityFrameworkCore;
using Project_Backend.Models;

namespace Project_Backend.Db_Context
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
        }

        //DB set
        public DbSet<Orders> Orders { get; set; }
    }
}