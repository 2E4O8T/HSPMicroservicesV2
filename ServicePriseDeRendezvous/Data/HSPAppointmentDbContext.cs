using Microsoft.EntityFrameworkCore;
using ServicePriseDeRendezvous.Models;

namespace ServicePriseDeRendezvous.Data
{
    public class HSPAppointmentDbContext : DbContext
    {
        public HSPAppointmentDbContext(DbContextOptions<HSPAppointmentDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Appointment>();
            modelBuilder.Entity<Patient>();
        }
    }
}
