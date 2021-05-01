using DistroLab2.Areas.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DistroLab2.Areas.Identity.Data
{
    /// <summary>
    /// Context class to create and access the Identity database
    /// </summary>
    public class LoginContext : IdentityDbContext<LoginUser>
    {
        public LoginContext(DbContextOptions<LoginContext> options)
            : base(options)
        {
        }
        public LoginContext()
        {

        }
  
         protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Identity;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<LoginDetails> LoginDetails { get; set; }    
    }
}
