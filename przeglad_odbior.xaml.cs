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
    /// Logika interakcji dla klasy przeglad_odbior.xaml
    /// </summary>
    public partial class przeglad_odbior : Window
    {
        public przeglad_odbior()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(NrREJ.Text)) MessageBox.Show("Sprawdz numer rejestracyjny...");
            else
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                conn.Open();

                string exist = $"select count(pojazd.pojazd_id) from tesla.pojazd where nr_rejestracyjny='{NrREJ.Text}'";

                MySqlCommand comm = new MySqlCommand(exist, conn);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if (System.Convert.ToInt32(czy_istnieje) != 1)
                {
                    MessageBox.Show("UWAGA!! W bazie nie istnieje pojazd o podanym numerze rejestracyjnym !!!");
                }
                else
                {
                    string maitenaceyes = $"select `Zlecony_przeglad` from `tesla`.`pojazd` WHERE tesla.pojazd.nr_rejestracyjny ='{NrREJ.Text}'";
                    comm = new MySqlCommand(maitenaceyes, conn);

                    if (comm.ExecuteScalar().ToString() == "False")
                    {
                        MessageBox.Show("UWAGA ! Pojazd o podanym numerze rejestracyjnym nie jest mozliwy do odbioru z przegladu!");
                    }
                    else
                    {
                        string maitenance = $"UPDATE `tesla`.`pojazd` SET `Zlecony_przeglad`= '0' WHERE tesla.pojazd.nr_rejestracyjny ='{NrREJ.Text}'";

                        comm = new MySqlCommand(maitenance, conn);

                        var a = comm.ExecuteNonQuery();
                        if (a == 1)
                        {
                            MessageBox.Show("Pojazd odebrany z przegladu.");
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
}
