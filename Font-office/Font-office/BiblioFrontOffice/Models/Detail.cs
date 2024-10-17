using Microsoft.EntityFrameworkCore;

namespace BiblioFrontOffice.Models
{
    public class Detail
    {

        public int Id { get; set; }
        public int idDocument { get; set; }
        public int idReservation { get; set; }

        public Detail( int idDocument, int idReservation)
        {
            this.idDocument = idDocument;
            this.idReservation = idReservation;
        }
    }
}
