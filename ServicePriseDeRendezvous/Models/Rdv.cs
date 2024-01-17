namespace ServicePriseDeRendezvous.Models
{
    public class Rdv
    {
        public int Id { get; set; }
        public string NomDuPatient { get; set; }
        public string ObjetConsultation { get; set; }
        public DateTime JourRendezVous { get; set; }
    }
}
