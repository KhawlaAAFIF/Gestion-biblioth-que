using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MiniProjeteBibliotheque
{
    /// <summary>
    /// Interaction logic for GestEmployes.xaml
    /// </summary>
    public partial class GestEmployes : Window
    {

        BiblioDataDataContext dbContext;
        public GestEmployes()
        {
            InitializeComponent();
            dbContext = new BiblioDataDataContext();
        }

        public void RefreshData()
        {
            var user = (from l in dbContext.Utilisateurs select l).ToList();
            EmployeDataGrid.ItemsSource = user;
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
     

        //--------Insert--------------
        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            // Show the form and hide the DataGrid when btnHide is clicked
            myForm.Visibility = Visibility.Visible;
            EmployeDataGrid.Visibility = Visibility.Collapsed;
            btnHide.Visibility = Visibility.Collapsed;
            btnUpdateLivre.Visibility = Visibility.Collapsed;
            gridChercher.Visibility = Visibility.Collapsed;
            gridChercherTxt.Visibility = Visibility.Collapsed;
        }
        private void btnAddEmploye_Click(object sender, RoutedEventArgs e)
        {
            string nom = txNom.Text;
            string prenom = txPrenom.Text;
            string email = txtemail.Text;
            string password = pswd.Password;
            string role = "Employe";

            if (IsValidEmail(email))
            {
                // L'e-mail est valide

                MessageBox.Show("Ajout réussi!");
            }
            else
            {
                MessageBox.Show("Veuillez saisir une adresse e-mail valide.");
                // Empêcher la fermeture de la fenêtre
                e.Handled = true;
                return;
            }

            using (BiblioDataDataContext context = new BiblioDataDataContext())
            {
                Utilisateur newUtilisateur = new Utilisateur
                {
                    nom = nom,
                    prenom = prenom,
                    email = email,
                    password = password,
                    role = role
                };

                context.Utilisateurs.InsertOnSubmit(newUtilisateur);
                context.SubmitChanges();

                RefreshData();
            }
            txNom.Text = string.Empty;
            txPrenom.Text = string.Empty;
            txtemail.Text = string.Empty;

            myForm.Visibility = Visibility.Collapsed;
            EmployeDataGrid.Visibility = Visibility.Visible;
            btnHide.Visibility = Visibility.Visible;
            btnUpdateLivre.Visibility = Visibility.Visible;
            gridChercher.Visibility = Visibility.Visible;
            gridChercherTxt.Visibility = Visibility.Visible;

        }



        //-----------Update-------------
        private Utilisateur selectedUtilisateur;
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeDataGrid.SelectedItem != null)
            {
                selectedUtilisateur = (Utilisateur)EmployeDataGrid.SelectedItem;

                txNom.Text = selectedUtilisateur.nom;
                txPrenom.Text = selectedUtilisateur.prenom;
                txtemail.Text = selectedUtilisateur.email;
                pswd.Password = selectedUtilisateur.password;

                myForm.Visibility = Visibility.Visible;
                EmployeDataGrid.Visibility = Visibility.Collapsed;
                btnAddLivre.Visibility = Visibility.Collapsed;
                btnHide.Visibility = Visibility.Collapsed;
                gridChercher.Visibility = Visibility.Collapsed;
                gridChercherTxt.Visibility = Visibility.Collapsed;
            }
            
        }

        private void btnUpdateEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUtilisateur != null)
            {
                string updatedEmail = txtemail.Text.Trim();
                if (IsValidEmail(updatedEmail))
                {
                    selectedUtilisateur.nom = txNom.Text;
                    selectedUtilisateur.prenom = txPrenom.Text;
                    selectedUtilisateur.email = updatedEmail;
                    selectedUtilisateur.password= pswd.Password;

                    using (BiblioDataDataContext context = new BiblioDataDataContext())
                    {
                        Utilisateur UserToUpdate = context.Utilisateurs.FirstOrDefault(d => d.id == selectedUtilisateur.id);
                        if (UserToUpdate != null)
                        {
                            UserToUpdate.nom = selectedUtilisateur.nom;
                            UserToUpdate.prenom = selectedUtilisateur.prenom;
                            UserToUpdate.email = selectedUtilisateur.email;
                            UserToUpdate.password = selectedUtilisateur.password;

                            
                            context.SubmitChanges();

                            RefreshData();
                        }
                    }

                    txNom.Text = string.Empty;
                    txPrenom.Text = string.Empty;
                    txtemail.Text = string.Empty;
                    pswd.Password = string.Empty;

                    myForm.Visibility = Visibility.Collapsed;
                    EmployeDataGrid.Visibility = Visibility.Visible;
                    btnAddLivre.Visibility = Visibility.Visible;
                    btnHide.Visibility = Visibility.Visible;
                    gridChercher.Visibility = Visibility.Visible;
                    gridChercherTxt.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Veuillez saisir une adresse e-mail valide.");
                }
            }
        }


        //---------------delete--------------
        public void DeleteUtilisateur(int utilisateurId)
        {
            var utilisateur = dbContext.Utilisateurs.FirstOrDefault(d => d.id == utilisateurId);
            if (utilisateur != null)
            {
                dbContext.Utilisateurs.DeleteOnSubmit(utilisateur);
                dbContext.SubmitChanges();

                RefreshData();
            }

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                var user = btn.DataContext as Utilisateur;
                if (user != null)
                {
                    var messageBoxResult = MessageBox.Show("Êtes-vous sûr(e) de vouloir supprimer cet employé?", "Supprimer employé", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        DeleteUtilisateur(user.id);
                    }
                }
            }
        }

        //---------Delete ALL -----------------------
        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            var itemsToDelete = new List<Utilisateur>();

            foreach (var item in EmployeDataGrid.Items)
            {
                var row = EmployeDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    CheckBox checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null && checkBox.IsChecked == true)
                    {
                        // Récupérer l'élément utilisateur correspondant à la ligne sélectionnée
                        var utilisateur = item as Utilisateur;
                        if (utilisateur != null)
                        {
                            itemsToDelete.Add(utilisateur);
                        }
                    }
                }
            }

            // Supprimer les éléments sélectionnés de la base de données
            using (var context = new BiblioDataDataContext())
            {
                if (itemsToDelete.Any()) {
                    var messageBoxResult = MessageBox.Show("Êtes-vous sûr(e)?", "Supprimer livre", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes) { 
                        foreach (var utilisateur in itemsToDelete)
                        {
                            var utilToDelete = context.Utilisateurs.FirstOrDefault(d => d.id == utilisateur.id);
                            if (utilToDelete != null)
                            {
                                context.Utilisateurs.DeleteOnSubmit(utilToDelete);
                            }
                        }
                    }
                }

                context.SubmitChanges();
            }

            RefreshData(); // Rafraîchir le DataGrid après la suppression
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in EmployeDataGrid.Items)
            {
                var row = EmployeDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    CheckBox checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null)
                    {
                        checkBox.IsChecked = true;
                    }
                }
            }
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            // Lorsque la case "Sélectionner tout" est décochée, décocher toutes les cases du DataGrid
            foreach (var item in EmployeDataGrid.Items)
            {
                var row = EmployeDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    CheckBox checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null)
                    {
                        checkBox.IsChecked = false;
                    }
                }
            }
        }
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }


        // Méthode pour valider l'e-mail avec une expression régulière
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = txtFilter.Text;
            var dataView = CollectionViewSource.GetDefaultView(EmployeDataGrid.ItemsSource);
            dataView.Filter = item =>
            {

                var yourModel = item as Utilisateur;
                if (yourModel != null)
                {
                    int intValue;
                    bool isInt = int.TryParse(filterText, out intValue);

                  
                   if (isInt)
                    {
                        return yourModel.id.ToString().ToLower().Contains(filterText.ToLower());
                    }
                    else
                    {
                        return yourModel.nom.ToLower().Contains(filterText.ToLower()) || yourModel.prenom.ToLower().Contains(filterText.ToLower()) || yourModel.email.ToLower().Contains(filterText.ToLower());
                    }
                }

                return false;
            };
        }

    }
}
