using Microsoft.EntityFrameworkCore;
using ServicePriseDeRendezvous.Models;

namespace ServicePriseDeRendezvous.Data
{
    public class RdvsApiContext : DbContext
    {
        public RdvsApiContext(DbContextOptions<RdvsApiContext> options) : base(options) 
        {
        }

        public DbSet<Rdv> Rdvs { get; set; }
    }
}
