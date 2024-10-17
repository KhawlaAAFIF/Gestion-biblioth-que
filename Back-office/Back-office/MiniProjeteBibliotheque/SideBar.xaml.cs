using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MiniProjeteBibliotheque
{
    /// <summary>
    /// Interaction logic for SideBar.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {
        public SideBar()
        {
            InitializeComponent();
        }
       

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    label_nom.Content = "";
        //    int id = MainWindow.id;
        //    BiblioDataDataContext bd = new BiblioDataDataContext();
        //    var nomcomplet = (from u in bd.Utilisateurs where u.id == id select new { u.nom, u.prenom }).FirstOrDefault();
        //    string fullName = $"{nomcomplet.nom} {nomcomplet.prenom}";
        //    label_nom.Content = fullName;
        //}

        

        private void btn_livres_Click(object sender, RoutedEventArgs e)
        {
            (new GestLivres()).Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            foreach (Window window in Application.Current.Windows.OfType<Window>().Where(w => w != mainWindow))
            {
                window.Close();
            }
             (new MainWindow()).Show();
            mainWindow.Close();

        }

        private void btn_reserv_Click(object sender, RoutedEventArgs e)
        {
            (new GestReservations()).Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btn_adher_Click(object sender, RoutedEventArgs e)
        {
            (new GestAdherent()).Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void btn_emp_Click(object sender, RoutedEventArgs e)
        {
            (new GestEmployes()).Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}
