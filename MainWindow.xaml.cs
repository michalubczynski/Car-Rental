using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace Baza
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
            conn.Open();
            String query = "SELECT count(pojazd.model), cennik.cena, cennik.segment_pojazdu from pojazd,cennik,wynajem WHERE 'time'  NOT BETWEEN wynajem.data_od AND wynajem.data_do";
            MySqlCommand comm = new MySqlCommand(query, conn);
            string ile_aut_ava = comm.ExecuteScalar().ToString();

            String query2 = "SELECT DISTINCT pojazd.model from pojazd,cennik,wynajem WHERE 'time'  NOT BETWEEN wynajem.data_od AND wynajem.data_do";
            //tabControlAUTA.Items.Clear();
            MySqlCommand modeledostepne = new MySqlCommand(query2, conn);

            MySqlDataReader rdr = modeledostepne.ExecuteReader();




            conn.Close();

        }

        private void btn_RezerwujSamochody_Click(object sender, RoutedEventArgs e)
        {
            var window = new Rejestracja();

            window.Owner = this;
            window.Show();
        }


        private void btnLogowanie_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxEmail.Text.Contains("Pracownik."))
            {
                login_pracownik();
            }
            else if(txtBoxEmail.Text.Contains("Prezes"))
            {
                login_prezes();
            }
            else
            {
                login_klient();
            }

            void login_pracownik()
            {
                MySqlConnection worker = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                worker.Open();
                string exist_worker = $"Select count(pracownik.nr_umowy) from pracownik where pracownik.nr_dowodu_os='{txtBoxEmail.Text.Remove(0,10)}' and pracownik.haslo='{txtBoxHaslo.Password.ToString()}'";

                MySqlCommand comm = new MySqlCommand(exist_worker, worker);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if(System.Convert.ToInt32(czy_istnieje)!=1)
                {
                    MessageBox.Show("Wystąpił błąd w logowaniu... Sprawdź poprawność danych do logowania lub skontaktuj się z pomocą techniczną...");
                }
                else
                {
                    var window = new Administracja(txtBoxEmail.Text.Remove(0, 10));

                    window.Owner = this;
                    window.Show();
                }

            }

            void login_prezes()
            {
                MySqlConnection worker = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                worker.Open();
                string exist_prezes = $"Select count(pracownik.nr_umowy) from pracownik where pracownik.nr_dowodu_os='{txtBoxEmail.Text}' and pracownik.haslo='{txtBoxHaslo.Password.ToString()}'";

                MySqlCommand comm = new MySqlCommand(exist_prezes, worker);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if (System.Convert.ToInt32(czy_istnieje) != 1)
                {
                    MessageBox.Show("Wystąpił błąd w logowaniu... Sprawdź poprawność danych do logowania lub skontaktuj się z pomocą techniczną...");
                }
                else
                {
                    var window = new Panel_prezes();

                    window.Owner = this;
                    window.Show();
                }
            }

            void login_klient()
            {
                MySqlConnection client = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                client.Open();

                string exist_client = $"Select count(klient.id_klienta) from klient where klient.nr_dowodu_os ='{txtBoxEmail.Text}' and klient.haslo='{txtBoxHaslo.Password.ToString()}'";

                MySqlCommand comm = new MySqlCommand(exist_client, client);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if (System.Convert.ToInt32(czy_istnieje) != 1)
                {
                    MessageBox.Show("Wystąpił błąd w logowaniu... Sprawdź poprawność danych do logowania lub skontaktuj się z pomocą techniczną...");
                }
                else
                {
                    var window = new Klient_Administracja(txtBoxEmail.Text);

                    window.Owner = this;
                    window.Show();
                }
            }
        }
    }
}
