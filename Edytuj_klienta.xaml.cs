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
    /// Logika interakcji dla klasy Edytuj_klienta.xaml
    /// </summary>
    public partial class Edytuj_klienta : Window
    {
        public Edytuj_klienta()
        {
            InitializeComponent();
        }


        private bool CheckPoprawnoscdanych()
        {

            if (String.IsNullOrEmpty(IMIE.Text)) return false;
            if (String.IsNullOrEmpty(NAZWISKO.Text)) return false;
            if (String.IsNullOrEmpty(NRDOWODU.Text)) return false;
            if (String.IsNullOrEmpty(nr_tel.Text)) return false;
            if (String.IsNullOrEmpty(nr_prawa_jazdy.Text)) return false;
            if (String.IsNullOrEmpty(haslo.Text)) return false;
            return true;
        }


    private void Button_Click(object sender, RoutedEventArgs e)
        {

                if (CheckPoprawnoscdanych())
                {
                    MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                    conn.Open();

                    string exist = $"select count(klient.id_klienta) from tesla.klient where klient.nr_dowodu_os='{NRDOWODU.Text}'";

                    MySqlCommand comm = new MySqlCommand(exist, conn);

                    string czy_istnieje = comm.ExecuteScalar().ToString();

                    if (System.Convert.ToInt32(czy_istnieje) != 1)
                    {
                        MessageBox.Show("UWAGA!! W bazie nie istnieje klient o podanym numerze dowodu !!!");
                    }
                    else
                    {
                    comm = new MySqlCommand("SET SQL_SAFE_UPDATES = 0;", conn);
                    comm.ExecuteNonQuery(); // :)

                    string edit_client = $"UPDATE `tesla`.`klient` SET `nazwisko`='{NAZWISKO.Text}',`imie`='{IMIE.Text}',`nr_tel`= '{nr_tel.Text}',`nr_prawa_jazdy`='{nr_prawa_jazdy.Text}',`haslo`='{haslo.Text}' WHERE tesla.klient.nr_dowodu_os ='{NRDOWODU.Text}'";
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
