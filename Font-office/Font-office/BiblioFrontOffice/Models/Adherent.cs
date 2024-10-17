namespace BiblioFrontOffice.Models
{
    public class Adherent
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string password { get; set; }
/*        public string confirmPwd { get; set; }
*/       
        public DateTime dateinscription { get; set; }

    }
}
