using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAgendasDesPraticiens.Models
{
    [Table("ConsultantCalendar")]
    public class ConsultantCalendar
    {
        public int Id { get; set; }
        public DateTime StartDateTime {  get; set; }
        public DateTime EndDateTime { get; set; }
        public bool? Available { get; set; }

        [ForeignKey("Consultant")]
        public int ConsultantId { get; set; }
        public virtual Consultant Consultant { get; set; }
    }
}
