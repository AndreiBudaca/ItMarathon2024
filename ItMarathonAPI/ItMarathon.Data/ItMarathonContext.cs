using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Data
{
    public class ItMarathonContext : DbContext
    {
        public ItMarathonContext(DbContextOptions<ItMarathonContext> options) : base(options) { }
    }
}
