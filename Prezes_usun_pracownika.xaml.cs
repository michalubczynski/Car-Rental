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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
namespace Baza
{
    /// <summary>
    /// Logika interakcji dla klasy Prezes_usun_pracownika.xaml
    /// </summary>
    public partial class Prezes_usun_pracownika : Window
    {
        public Prezes_usun_pracownika()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
            conn.Open();

            string exist = $"select count(pracownik.pracownik_id) from tesla.pracownik where pracownik.nazwisko='{NAZWISKO.Text}' and pracownik.imie='{IMIE.Text}' AND pracownik.nr_dowodu_os='{NRDOWODU.Text}'";

            MySqlCommand comm = new MySqlCommand(exist, conn);

            string czy_istnieje = comm.ExecuteScalar().ToString();

            if (System.Convert.ToInt32(czy_istnieje) != 1)
            {
                MessageBox.Show("Wystąpił błąd w identyfikacji Pracownika w bazie... Skontaktuj się z pomocą techniczną... ");
            }
            else
            {
                string delete_client = $"DELETE FROM TESLA.pracownik WHERE pracownik.nazwisko='{NAZWISKO.Text}' and pracownik.imie='{IMIE.Text}' AND pracownik.nr_dowodu_os='{NRDOWODU.Text}'";

                comm = new MySqlCommand(delete_client, conn);

                var a = comm.ExecuteNonQuery();
                if (a == 1)
                {
                    MessageBox.Show("Pracownik usunięty.");
                }
                else
                {
                    MessageBox.Show("Cos poszło nie tak...");
                }

            }

        }
    }
}
