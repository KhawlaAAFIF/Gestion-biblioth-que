namespace BiblioFrontOffice.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public DateTime Dateajout { get; set; }
        public int Stock { get; set; }
        public string NomAuteur { get; set; }
      

    }
}
