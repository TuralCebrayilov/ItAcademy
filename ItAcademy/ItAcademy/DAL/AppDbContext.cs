using ItAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace ItAcademy.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        { 
        }
       
        public DbSet<Students> Students { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Employers> Employers { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Positions> Positions { get; set; }
    }
}
