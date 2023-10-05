using System.Security.Cryptography.X509Certificates;
using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)//ctor
        {

        }
        public DbSet<Category>Categories{ get;set; }
    }
}
