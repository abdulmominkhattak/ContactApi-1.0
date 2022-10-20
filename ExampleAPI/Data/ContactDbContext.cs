using ExampleAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleAPI.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
    }
}
