using Microsoft.EntityFrameworkCore;
using Namocorp_Contacts_Manager.Models.Domain;

namespace Namocorp_Contacts_Manager.Data
{
    public class NamocorpContactsManagerDbContext : DbContext
    {
        public  NamocorpContactsManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
