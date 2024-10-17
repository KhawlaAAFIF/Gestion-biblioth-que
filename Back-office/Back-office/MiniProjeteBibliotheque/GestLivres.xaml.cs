using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace MiniProjeteBibliotheque
{
    
    public partial class GestLivres : Window
    {
        BiblioDataDataContext dbContext;
        public GestLivres()
        {
            InitializeComponent();
            dbContext = new BiblioDataDataContext();

        }
        SqlConnection cn = new SqlConnection(@"Server=(local);Database=MiniProjetBiblio;Trusted_Connection=True;");


        public void RefreshData()
        {
            var livres = (from l in dbContext.Documents select l).ToList();
            livreDataGrid.ItemsSource = livres;
        }
        public void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
        //--------Insert--------------
        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            myForm.Visibility = Visibility.Visible;
            livreDataGrid.Visibility = Visibility.Collapsed;
            btnHide.Visibility = Visibility.Collapsed;
            btnUpdateLivre.Visibility = Visibility.Collapsed;
            DateI.Visibility = Visibility.Collapsed;
            gridChercher.Visibility = Visibility.Collapsed;
            gridChercherTxt.Visibility = Visibility.Collapsed;
        }
        private void btnAddLivre_Click(object sender, RoutedEventArgs e)
        {
            string libelle = txLib.Text;
            string auteur = txAuteur.Text;
            DateTime dateAjout = DateTime.Now;
            int stock;
            if (int.TryParse(txtStoc.Text, out stock) == false)
            {
                MessageBox.Show("stock doit être un entier");
                return;
            }
            

            using (BiblioDataDataContext context = new BiblioDataDataContext())
            {
                Document newDocument = new Document
                {
                    libelle = libelle,
                    nomAuteur = auteur,
                    dateajout = dateAjout,
                    stock = stock
                };

                context.Documents.InsertOnSubmit(newDocument);
                context.SubmitChanges();

                RefreshData();
            }
            txLib.Text = string.Empty;
            txAuteur.Text = string.Empty;
            txtStoc.Text = string.Empty;

            myForm.Visibility = Visibility.Collapsed;
            livreDataGrid.Visibility = Visibility.Visible;
            btnHide.Visibility = Visibility.Visible;
            btnUpdateLivre.Visibility = Visibility.Visible;
            DateI.Visibility = Visibility.Visible;
            gridChercher.Visibility = Visibility.Visible;
            gridChercherTxt.Visibility = Visibility.Visible;

        }



        //-----------Update-------------
        private Document selectedDocument;
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (livreDataGrid.SelectedItem != null)
            {
                selectedDocument = (Document)livreDataGrid.SelectedItem;

                txLib.Text = selectedDocument.libelle;
                txAuteur.Text = selectedDocument.nomAuteur;
                if (selectedDocument.dateajout is DateTime date)
                {
                    txtDate.Text = date.ToString("dd/MM/yyyy");
                }
                txtStoc.Text = selectedDocument.stock.ToString();

                // Afficher le formulaire et masquer le DataGrid
                myForm.Visibility = Visibility.Visible;
                livreDataGrid.Visibility = Visibility.Collapsed;
                btnAddLivre.Visibility = Visibility.Collapsed;
                btnHide.Visibility= Visibility.Collapsed;
                gridChercher.Visibility= Visibility.Collapsed;
                gridChercherTxt.Visibility= Visibility.Collapsed;
            }
        }

        private void btnUpdateLivre_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDocument != null)
            {
                selectedDocument.libelle = txLib.Text;
                selectedDocument.nomAuteur = txAuteur.Text;
           
                int newStock;
                if (int.TryParse(txtStoc.Text, out newStock))
                {
                    selectedDocument.stock = newStock;
                }
                else
                {
                    MessageBox.Show("stock doit être un etier");
                    return;
                }
                // Mise à jour dans la base de données
                using (BiblioDataDataContext context = new BiblioDataDataContext())
                {
                    Document documentToUpdate = context.Documents.FirstOrDefault(d => d.id == selectedDocument.id);
                    if (documentToUpdate != null)
                    {
                        documentToUpdate.libelle = selectedDocument.libelle;
                        documentToUpdate.nomAuteur = selectedDocument.nomAuteur;
                        documentToUpdate.stock = selectedDocument.stock;

                        context.SubmitChanges();

                        RefreshData();
                    }
                }

                txLib.Text = string.Empty;
                txAuteur.Text = string.Empty;
                txtStoc.Text = string.Empty;

                myForm.Visibility = Visibility.Collapsed;
                livreDataGrid.Visibility = Visibility.Visible;
                btnAddLivre.Visibility = Visibility.Visible;
                btnHide.Visibility = Visibility.Visible;
                gridChercher.Visibility = Visibility.Visible;
                gridChercherTxt.Visibility = Visibility.Visible;
            }
        }



        //---------------delete--------------
        public void DeleteDocument(int documentId)
        {
            var document = dbContext.Documents.FirstOrDefault(d => d.id == documentId);
            if (document != null)
            {
                dbContext.Documents.DeleteOnSubmit(document);
                dbContext.SubmitChanges();

                RefreshData();
            }

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                var document = btn.DataContext as Document;
                if (document != null)
                {
                    var messageBoxResult = MessageBox.Show("Êtes-vous sûr(e) de vouloir supprimer ce livre?", "Supprimer livre", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        DeleteDocument(document.id);
                    }
                }
            }
        }
        
        //---------Delete ALL -----------------------
        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            var itemsToDelete = new List<Document>();

            foreach (var item in livreDataGrid.Items)
            {
                var row = livreDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    CheckBox checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null && checkBox.IsChecked == true)
                    {
                        var document = item as Document;
                        if (document != null)
                        {
                            itemsToDelete.Add(document);
                        }
                    }
                }
            }
            // Suppression des éléments sélectionnés de la base de données
            
            using (var context = new BiblioDataDataContext())
            {
                if (itemsToDelete.Any())
                {
                    var messageBoxResult = MessageBox.Show("Êtes-vous sûr(e)?", "Supprimer livre", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        foreach (var document in itemsToDelete)
                        {
                            var docToDelete = context.Documents.FirstOrDefault(d => d.id == document.id);
                            if (docToDelete != null)
                            {

                                context.Documents.DeleteOnSubmit(docToDelete);
                            }
                        }
                    }
                }
                context.SubmitChanges();
            }

            RefreshData();
        }

        //----------------check all-------------------------
        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            // Lorsque la case "Sélectionner tout" est cochée, cocher toutes les cases du DataGrid
            foreach (var item in livreDataGrid.Items)
            {
                var row = livreDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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

        //--------------uncheck all-------------
        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            // Lorsque la case "Sélectionner tout" est décochée, décocher toutes les cases du DataGrid
            foreach (var item in livreDataGrid.Items)
            {
                var row = livreDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = txtFilter.Text;
            var dataView = CollectionViewSource.GetDefaultView(livreDataGrid.ItemsSource);
            dataView.Filter = item =>
            {

                var yourModel = item as Document;
                if (yourModel != null)
                {
                    DateTime dateValue;
                    bool isDate = DateTime.TryParse(filterText, out dateValue);

                    int intValue;
                    bool isInt = int.TryParse(filterText, out intValue);

                    if (isDate)
                    {
                        return yourModel.dateajout.ToString().ToLower().Contains(filterText.ToLower());
                    }
                    else if (isInt)
                    {
                        return yourModel.stock.ToString().ToLower().Contains(filterText.ToLower()) || yourModel.id.ToString().ToLower().Contains(filterText.ToLower());
                    }
                    else
                    {
                        return yourModel.libelle.ToLower().Contains(filterText.ToLower()) || yourModel.nomAuteur.ToLower().Contains(filterText.ToLower());
                    }
                }

                return false;
            };
        }
    }

}

