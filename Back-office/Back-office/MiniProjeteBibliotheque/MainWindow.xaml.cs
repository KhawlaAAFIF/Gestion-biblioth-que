using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace MiniProjeteBibliotheque
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static int id;
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            vider();
            if (txt_email.Text != "" && txt_pass.Password.ToString() != "")
            {
                string email = txt_email.Text;
                string password = txt_pass.Password.ToString();
                BiblioDataDataContext bibl_data = new BiblioDataDataContext();
                var list = (from u in bibl_data.Utilisateurs
                            where u.email == email & u.password == password
                            select u).ToList();

                if (list.Count != 0)
                {
                    id = list[0].id;
                    (new Dashboard()).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Email ou mot de passe invalid");
                }
            }
            if (txt_email.Text == "")
            {
                err_email.Content = "Email Obligatoire";
            }
            if (txt_pass.Password.ToString() == "")
            {
                err_password.Content = "Mot de passe Obligatoire";

            }

        }

        void vider()
        {
            err_email.Content = err_password.Content = "";

        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //(new GestReservations()).Show();
            vider();
        }
    }
}
