using DistroLab2.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Connects into the message database. 
/// </summary>
namespace DistroLab2.Data
{
    public class MessageDbContext : DbContext
    {

        /// <summary>
        /// A config that locates the message database
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=MessageDb;Trusted_Connection=True;");

        /// <summary>
        /// A database set of messages
        /// </summary>
        public DbSet<Message> Messages { get; set; }
    }
}
