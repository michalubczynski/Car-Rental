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
    /// Logika interakcji dla klasy Prezes_edytuj_pracownika.xaml
    /// </summary>
    public partial class Prezes_edytuj_pracownika : Window
    {
        public Prezes_edytuj_pracownika()
        {
            InitializeComponent();
        }

        private bool CheckPoprawnoscdanych()
        {
            if (String.IsNullOrEmpty(IMIE.Text)) return false;
            if (String.IsNullOrEmpty(NAZWISKO.Text)) return false;
            if (String.IsNullOrEmpty(NRDOWODU.Text)) return false;
            if (String.IsNullOrEmpty(nr_tel.Text)) return false;
            if (String.IsNullOrEmpty(haslo.Text)) return false;
            return true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (CheckPoprawnoscdanych())
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                conn.Open();

                string exist = $"select count(pracownik.pracownik_id) from tesla.pracownik where pracownik.nr_dowodu_os='{NRDOWODU.Text}'";

                MySqlCommand comm = new MySqlCommand(exist, conn);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if (System.Convert.ToInt32(czy_istnieje) != 1)
                {
                    MessageBox.Show("UWAGA!! W bazie nie istnieje pracownik o podanym numerze dowodu !!!");
                }
                else
                {
                    comm = new MySqlCommand("SET SQL_SAFE_UPDATES = 0;", conn);
                    comm.ExecuteNonQuery(); // :)

                    string edit_client = $"UPDATE `tesla`.`pracownik` SET `nazwisko`='{NAZWISKO.Text}',`imie`='{IMIE.Text}',`nr_tel`= '{nr_tel.Text}',`haslo`='{haslo.Text}' WHERE tesla.pracownik.nr_dowodu_os ='{NRDOWODU.Text}'";

                    comm = new MySqlCommand(edit_client, conn);

                    var a = comm.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Dane zaktualizowane.");
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

