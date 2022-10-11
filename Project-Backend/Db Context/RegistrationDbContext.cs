using Microsoft.EntityFrameworkCore;
using Project_Backend.Models;
using System.Collections.Generic;

namespace Project_Backend.Db_Context
{
    public class RegistrationDbContext : DbContext
    {
        public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options) : base(options)
        {
        }

        //Dbset
        public DbSet<Registration> Registration { get; set; }
    }
}