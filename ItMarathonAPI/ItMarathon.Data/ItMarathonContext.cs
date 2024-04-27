using ItMarathon.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Data
{
    public class ItMarathonContext : DbContext
    {
        public ItMarathonContext(DbContextOptions<ItMarathonContext> options) : base(options) { }

        public DbSet<HelloWorld> HelloWorlds { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
