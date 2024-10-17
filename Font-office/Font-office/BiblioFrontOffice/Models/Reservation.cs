namespace BiblioFrontOffice.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime DateDebutEmprunt { get; set; }
        public DateTime DateFinEmprunt { get; set; }
        public int Status { get; set;}
        public int IdAdherent {  get; set; }

    }
}
