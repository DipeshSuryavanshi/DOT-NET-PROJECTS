using Microsoft.EntityFrameworkCore;
using RegistrationPage.Model;


namespace RegistrationPage.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserRegistration> userRegistrations { get; set; } 
    }
}
