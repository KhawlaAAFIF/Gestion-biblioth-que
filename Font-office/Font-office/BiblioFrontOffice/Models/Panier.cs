using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BiblioFrontOffice.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public int IdAdherent { get; set; }
        public int IdDocument { get; set; }

        public Panier( int idAdherent, int idDocument)
        {
            IdAdherent = idAdherent;
            IdDocument = idDocument;
        }
    }
}
