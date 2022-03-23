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

    public partial class Nowy_klient : Window
    {
        public Nowy_klient()
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

                string exist = $"select count(klient.id_klienta) from tesla.klient where KLIENT.nazwisko='{NAZWISKO.Text}' and klient.imie='{IMIE.Text}' AND klient.nr_dowodu_os='{NRDOWODU.Text}'";

                MySqlCommand comm = new MySqlCommand(exist, conn);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if (System.Convert.ToInt32(czy_istnieje) == 1)
                {
                    MessageBox.Show("UWAGA!! W bazie już istnieje klient o podanym imieniu, nazwisku i numerze dowodu !!!");
                }
                else
                {
                    string New_client = $"insert into `tesla`.`klient` (nazwisko,imie,nr_dowodu_os,nr_tel,nr_prawa_jazdy,haslo) VALUES('{NAZWISKO.Text}','{IMIE.Text}','{NRDOWODU.Text}','{nr_tel.Text}','{nr_prawa_jazdy.Text}','{haslo.Text}')";

                    comm = new MySqlCommand(New_client, conn);

                    var a = comm.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Klient dodany.");
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
