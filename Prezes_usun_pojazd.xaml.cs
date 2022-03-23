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

    public partial class Prezes_usun_pojazd : Window
    {
        public Prezes_usun_pojazd()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(rejestracja.Text))
            {
                MessageBox.Show("Sprawdź poprawność wprowadzanych danych");
            }
            else
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                conn.Open();

                string exist = $"select count(pojazd.pojazd_id) from tesla.pojazd where pojazd.nr_rejestracyjny='{rejestracja.Text}'";

                MySqlCommand comm = new MySqlCommand(exist, conn);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if (System.Convert.ToInt32(czy_istnieje) != 1)
                {
                    MessageBox.Show("Wystąpił błąd w identyfikacji pojazdu w bazie... Skontaktuj się z pomocą techniczną... ");
                }
                else
                {
                    string delete_client = $"DELETE FROM TESLA.pojazd WHERE pojazd.nr_rejestracyjny='{rejestracja.Text}'";

                    comm = new MySqlCommand(delete_client, conn);

                    var a = comm.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Pojazd usunięty.");
                    }
                    else
                    {
                        MessageBox.Show("Cos poszło nie tak...");
                    }

                }
            }

        }
    }
}
