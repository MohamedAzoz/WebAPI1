using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI1.Model;

namespace WebAPI1.Data
{
    public class AppDbContext :IdentityDbContext<AppUser>
    { 
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options){}

    }
}
