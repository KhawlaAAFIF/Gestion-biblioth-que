using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MiniProjeteBibliotheque
{
    /// <summary>
    /// Interaction logic for GestReservations.xaml
    /// </summary>
    public partial class GestReservations : Window
    {
        public GestReservations()
        {
            InitializeComponent();
        }

        void RefreshReservations()
        {
            BiblioDataDataContext bibliodata = new BiblioDataDataContext();

            var reservations = (from r in bibliodata.v_detail_reservations
                                select new { r.id, r.datereservation, r.status, r.color }
                              ).Distinct().ToList();
            dg_reservation_distinct.ItemsSource = reservations;

        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dg_reservations.Visibility = Visibility.Hidden;
            dg_reservation_distinct.Visibility = Visibility.Visible;
            update_form.Visibility = Visibility.Hidden;
            grid_reservations.Visibility = Visibility.Visible;
            BiblioDataDataContext bibliodata = new BiblioDataDataContext();
            var status = (from s in bibliodata.status_reservations select s);
            cb_etat.ItemsSource= status;
            cb_etat.DisplayMemberPath = "libelle";
            cb_etat.SelectedValuePath = "id";

            var reservations = (from r in bibliodata.v_detail_reservations
                                select new { r.id,r.datereservation,r.status,r.color } 
                              ).Distinct().ToList();
            //MessageBox.Show(reservations.Count()+"");
            dg_reservation_distinct.ItemsSource = reservations;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            //cb_etat.Text = ((Button)sender).Content.ToString();
            hidden_res_id.Content = ((Button)sender).Tag;
            update_form.Visibility = Visibility.Visible;
            dg_reservation_distinct.Visibility = Visibility.Hidden;

        }

        private void cb_etat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(cb_etat.SelectedIndex != -1)
            {
                //var selectedRow = dg_reservation_distinct.SelectedItem;
                //MessageBox.Show(selectedRow.ToString());
                //MessageBox.Show(selectedRow.Cou());
                int id = (int)hidden_res_id.Content;

               
                //v_detail_reservation selectedRow = (v_detail_reservation)dg_reservation_distinct.SelectedItem;

                //MessageBox.Show(selectedRow.id + "");
                BiblioDataDataContext bd = new BiblioDataDataContext();
                //Reservation r= bd.Reservations.Single(item=>item.id == selectedRow.id);
                Reservation r= bd.Reservations.Single(item=>item.id == id);
                r.status =(int) cb_etat.SelectedValue;
                bd.SubmitChanges();
                MessageBox.Show("Status Modifié");
                RefreshReservations();
                update_form.Visibility = Visibility.Hidden;
                dg_reservation_distinct.Visibility = Visibility.Visible;
               
            }
        }

        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirmer la suppression?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Suppression En cours.");
                int id = (int)((Button)sender).Tag;
                //v_detail_reservation selectedRow = (v_detail_reservation)dg_reservations.SelectedItem;
                BiblioDataDataContext bd = new BiblioDataDataContext();
                Reservation r = bd.Reservations.Single(item => item.id == id);
                int idres = r.id;
                //MessageBox.Show(idres + "");
                var details = (from d in bd.Details where d.idReservation == idres select d).ToList();
                foreach (var d in details)
                {

                    bd.Details.DeleteOnSubmit(d);
                }
                bd.SubmitChanges();
                bd.Reservations.DeleteOnSubmit(r);
                bd.SubmitChanges();

                RefreshReservations();

            }
            else
            {
                MessageBox.Show("Suppression annulé.");
            }
            
        }

        private void btnmore_Click(object sender, RoutedEventArgs e)
        {
            tb_title.Text = "Detail Reservation";
            btnDeleteAll.Visibility = Visibility.Hidden;
            int id=(int)((Button)sender).Tag;
            BiblioDataDataContext bd = new BiblioDataDataContext();
             var det_res = (from r in bd.v_detail_reservations where r.id ==id
                                                   select r
                    ).ToList();
            dg_reservations.ItemsSource= det_res;
            dg_reservation_distinct.Visibility = Visibility.Hidden;
            dg_reservations.Visibility = Visibility.Visible;

        }
    }
}
