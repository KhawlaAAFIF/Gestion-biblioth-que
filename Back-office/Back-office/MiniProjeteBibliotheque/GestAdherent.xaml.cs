using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
using System.IO;

namespace MiniProjeteBibliotheque
{
    /// <summary>
    /// Interaction logic for GestAdherent.xaml
    /// </summary>
    public partial class GestAdherent : Window
    {
        BiblioDataDataContext dbContext;
        public GestAdherent()
        {
            InitializeComponent();
            dbContext = new BiblioDataDataContext();

        }



        public void RefreshData()
        {
            var Adherent = (from l in dbContext.Adherents select l).ToList();
            AdherentDataGrid.ItemsSource = Adherent;
        }
        public void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }


        //--------Insert--------------
        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            myForm.Visibility = Visibility.Visible;
            AdherentDataGrid.Visibility = Visibility.Collapsed;
            btnHide.Visibility = Visibility.Collapsed;
            btnUpdateAdherent.Visibility = Visibility.Collapsed;
            DateI.Visibility = Visibility.Collapsed;
            gridChercher.Visibility = Visibility.Collapsed;
            gridChercherTxt.Visibility = Visibility.Collapsed;
        }
        private void btnAddAdherent_Click(object sender, RoutedEventArgs e)
        {
            string nom = txtNom.Text;
            string prenom = txtPrn.Text;
            string email = txtEmail.Text;
            string Password = txtpass.Password;
            DateTime dateinscription= DateTime.Now;
            if (IsValidEmail(email))
            {
                // L'e-mail est valide

                MessageBox.Show("Ajout réussi!");
            }
            else
            {
                // L'e-mail n'est pas valide, afficher un message d'erreur
                MessageBox.Show("Veuillez saisir une adresse e-mail valide.");
                // Empêcher la fermeture de la fenêtre
                return;
            }
           

            using (BiblioDataDataContext context = new BiblioDataDataContext())
            {
                Adherent newAdherent = new Adherent
                {
                    nom = nom,
                    prenom = prenom,
                    email = email,
                    password = Password,
                    dateinscription = dateinscription
                };

                context.Adherents.InsertOnSubmit(newAdherent);
                context.SubmitChanges();

                RefreshData();
            }
            txtNom.Text = string.Empty;
            txtPrn.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtpass.Password = string.Empty;

            myForm.Visibility = Visibility.Collapsed;
            AdherentDataGrid.Visibility = Visibility.Visible;
            btnHide.Visibility = Visibility.Visible;
            btnUpdateAdherent.Visibility = Visibility.Visible;
            DateI.Visibility = Visibility.Visible;
            gridChercher.Visibility = Visibility.Visible;
            gridChercherTxt.Visibility = Visibility.Visible;
        }



        //-----------Update-------------
        private Adherent selectedAdherent;
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (AdherentDataGrid.SelectedItem != null)
            {
                selectedAdherent = (Adherent)AdherentDataGrid.SelectedItem;

                // Remplir le formulaire avec les données ds l'adherent sélectionné
                txtNom.Text = selectedAdherent.nom;
                txtPrn.Text = selectedAdherent.prenom;
                txtEmail.Text = selectedAdherent.email; 
                txtpass.Password = selectedAdherent.password;
                if (selectedAdherent.dateinscription is DateTime date)
                {
                    txtDate.Text = date.ToString("dd/MM/yyyy");
                }
               

                // Afficher le formulaire et masquer le DataGrid
                myForm.Visibility = Visibility.Visible;
                AdherentDataGrid.Visibility = Visibility.Collapsed;
                btnAddAdherent.Visibility = Visibility.Collapsed;
                btnHide.Visibility = Visibility.Collapsed;
                gridChercher.Visibility = Visibility.Collapsed;
                gridChercherTxt.Visibility = Visibility.Collapsed;
            }
        }

        private void btnUpdateAdherent_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAdherent != null)
            {
                // Valider email
                string updatedEmail = txtEmail.Text.Trim();
                if (IsValidEmail(updatedEmail))
                {
                    selectedAdherent.nom = txtNom.Text;
                    selectedAdherent.prenom = txtPrn.Text;
                    selectedAdherent.email = updatedEmail;

                    
                    using (BiblioDataDataContext context = new BiblioDataDataContext())
                    {
                        Utilisateur UserToUpdate = context.Utilisateurs.FirstOrDefault(d => d.id == selectedAdherent.id);
                        if (UserToUpdate != null)
                        {
                            UserToUpdate.nom = selectedAdherent.nom;
                            UserToUpdate.prenom = selectedAdherent.prenom;
                            UserToUpdate.email = selectedAdherent.email;
                            UserToUpdate.password = selectedAdherent.password;

                            
                            context.SubmitChanges();

                            RefreshData();
                        }
                    }

                    txtNom.Text = string.Empty;
                    txtPrn.Text = string.Empty;
                    txtEmail.Text = string.Empty;

                    myForm.Visibility = Visibility.Collapsed;
                    AdherentDataGrid.Visibility = Visibility.Visible;
                    btnAddAdherent.Visibility = Visibility.Visible;
                    btnHide.Visibility = Visibility.Visible;
                    gridChercher.Visibility = Visibility.Visible;
                    gridChercherTxt.Visibility = Visibility.Visible;
                }
                else
                {
                    // Show an error message for invalid email
                    MessageBox.Show("Veuillez saisir une adresse e-mail valide.");
                }
            }
        }


        //---------------delete--------------
        public void DeleteAdherent(int adherentId)
        {
            var adherent = dbContext.Adherents.FirstOrDefault(d => d.id == adherentId);
            if (adherent != null)
            {
                dbContext.Adherents.DeleteOnSubmit(adherent);
                dbContext.SubmitChanges();

                RefreshData();
            }

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                var adherent = btn.DataContext as Adherent;
                if (adherent != null)
                {
                    var messageBoxResult = MessageBox.Show("Êtes-vous sûr(e) de vouloir supprimer l'adhérent selectionné?", "Supprimer livre", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        DeleteAdherent(adherent.id);
                    }
                }
            }
        }
        
        
        //---------Delete ALL -----------------------
        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            var itemsToDelete = new List<Adherent>();

            foreach (var item in AdherentDataGrid.Items)
            {
                var row = AdherentDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    CheckBox checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null && checkBox.IsChecked == true)
                    {
                        // Récupérer l'adherent correspondant à la ligne sélectionnée
                        var adherent = item as Adherent;
                        if (adherent != null)
                        {
                            itemsToDelete.Add(adherent);
                        }
                    }
                }
            }

            // Supprimer les éléments sélectionnés de la base de données
            using (var context = new BiblioDataDataContext())
            {

                if (itemsToDelete.Any())
                {
                    var messageBoxResult = MessageBox.Show("Êtes-vous sûr(e) de vouloir supprimer ces éléments?", "Supprimer adhérent", MessageBoxButton.YesNo);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        foreach (var adherent in itemsToDelete)
                        {
                            var docToDelete = context.Adherents.FirstOrDefault(d => d.id == adherent.id);
                            if (docToDelete != null)
                            {
                                context.Adherents.DeleteOnSubmit(docToDelete);
                            }
                        }

                        context.SubmitChanges(); 
                    }
                }
            }

            RefreshData(); 
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in AdherentDataGrid.Items)
            {
                var row = AdherentDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
            foreach (var item in AdherentDataGrid.Items)
            {
                var row = AdherentDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
            var dataView = CollectionViewSource.GetDefaultView(AdherentDataGrid.ItemsSource);
            dataView.Filter = item =>
            {

                var yourModel = item as Adherent;
                if (yourModel != null)
                {
                    DateTime dateValue;
                    bool isDate = DateTime.TryParse(filterText, out dateValue);

                    int intValue;
                    bool isInt = int.TryParse(filterText, out intValue);

                    if (isDate)
                    {
                        return yourModel.dateinscription.ToString().ToLower().Contains(filterText.ToLower());
                    }
                    else if (isInt)
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

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var data = (from a in dbContext.Adherents select a).ToList();

            // Check if there is data to export
            if (data == null || !data.Any())
            {
                MessageBox.Show("No data to export.");
                return;
            }

            // Convert the data to a CSV-formatted string
            var csvStringBuilder = new StringBuilder();
            csvStringBuilder.AppendLine("#,Prenom,Nom,Email,Date Inscription"); // Replace with your actual column headers

            foreach (var item in data)
            {
                csvStringBuilder.AppendLine($"{item.id},{item.prenom},{item.nom},{item.email},{item.dateinscription}");
                // Replace with the properties of your data class
            }

            // Use SaveFileDialog to let the user choose where to save the file
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                DefaultExt = ".csv",
                FileName = "ExporterAdherents"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                // Write the CSV string to the selected file
                File.WriteAllText(saveFileDialog.FileName, csvStringBuilder.ToString());

                MessageBox.Show($"Data exported to {saveFileDialog.FileName} successfully!");
            }

        }
    }

}

