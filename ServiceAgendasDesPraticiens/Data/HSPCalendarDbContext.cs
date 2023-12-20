using Microsoft.EntityFrameworkCore;
using ServiceAgendasDesPraticiens.Models;

namespace ServiceAgendasDesPraticiens.Data
{
    public class HSPCalendarDbContext : DbContext
    {
        public HSPCalendarDbContext(DbContextOptions<HSPCalendarDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<ConsultantCalendar> ConsultantsCalendars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Consultant>();
            modelBuilder.Entity<ConsultantCalendar>();
        }
    }
}
