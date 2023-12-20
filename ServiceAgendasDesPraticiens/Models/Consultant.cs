using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAgendasDesPraticiens.Models
{
    [Table("Consultant")]
    public class Consultant
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string Speciality { get; set; }
    }
}
