using HSPMicroservicesV2.Models;
using Microsoft.EntityFrameworkCore;

namespace HSPMicroservicesV2.Data
{
    public class RdvsApiContext : DbContext
    {
        public RdvsApiContext(DbContextOptions<RdvsApiContext> options) : base(options)
        {

        }

        public DbSet<Rdv> Rdvs { get; set; }
    }
}
