using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentDetailsInDigitalPlatform.Models
{
    public class AppDBContext : IdentityDbContext 
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):
            base(options)
        {

        }

        
        public DbSet<Student> Students { get; set; }  

       
    }
}
