using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace ServicePriseDeRendezvous.Models
{
    [Table("Appointment")]
    public partial class Appointment
    {
        public int Id { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
