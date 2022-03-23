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
using System.Data;
namespace Baza
{
    /// <summary>
    /// Logika interakcji dla klasy Prezes_nowy_pracownik.xaml
    /// </summary>
    public partial class Prezes_nowy_pracownik : Window
    {
        public Prezes_nowy_pracownik()
        {
            InitializeComponent();
        }

        private bool CheckPoprawnoscdanych()
        {

            if (String.IsNullOrEmpty(Imie.Text)) return false;
            if (String.IsNullOrEmpty(Nazwisko.Text)) return false;
            if (String.IsNullOrEmpty(numer_telefonu.Text)) return false;
            if (String.IsNullOrEmpty(numer_umowy.Text)) return false;
            if (String.IsNullOrEmpty(numer_dowodu.Text)) return false;
            if (String.IsNullOrEmpty(haslo.Text)) return false;
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckPoprawnoscdanych())
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                conn.Open();

                string exist = $"select count(pracownik.pracownik_id) from tesla.pracownik where pracownik.nr_dowodu_os='{numer_dowodu.Text}'";

                MySqlCommand comm = new MySqlCommand(exist, conn);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if (System.Convert.ToInt32(czy_istnieje) == 1)
                {
                    MessageBox.Show("UWAGA!! W bazie już Pracownik o podanym numerze Dowodu osobistego");
                }
                else
                {
                    string New_worker = $"insert into `tesla`.`pracownik` (imie,nazwisko,nr_tel,nr_umowy,nr_dowodu_os,haslo) VALUES('{Imie.Text}','{Nazwisko.Text}','{numer_telefonu.Text}','{numer_umowy.Text}','{numer_dowodu.Text}','{haslo.Text}')";

                    comm = new MySqlCommand(New_worker, conn);

                    var a = comm.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Pracownik dodany.");
                    }
                    else
                    {
                        MessageBox.Show("Cos poszło nie tak...");
                    }

                }
            }
            else { MessageBox.Show("Sprawdź poprawność wprowadzonych danych...."); }
        }
    }
}
