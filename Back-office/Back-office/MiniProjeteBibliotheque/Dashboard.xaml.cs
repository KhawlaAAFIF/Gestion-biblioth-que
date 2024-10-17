using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiniProjeteBibliotheque
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    /// 
    public partial class Dashboard : Window
    {
        BiblioDataDataContext dataContext;
        public Dashboard()
        {
            InitializeComponent();
            dataContext = new BiblioDataDataContext();
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            var nbrAdherent = dataContext.Adherents.Count();
            nbrAdr.Text = $"Nombre d'adhérents :\n {nbrAdherent}";

            var nbrEmpl = dataContext.Utilisateurs.Count(u => u.role == "employe");
            nbrEmp.Text = $"Nombre d'employés : \n {nbrEmpl}";

            var nbrLivr = dataContext.Documents.Count();
            nbrLiv.Text = $"Nombre de livres : \n {nbrLivr}";

            var nbrResC = dataContext.Reservations.Count(r => r.status == 1);
            nbrRes.Text = $"Nombre de réservation en cours : \n {nbrResC}";
        }
    }
}
