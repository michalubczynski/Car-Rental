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
    /// Logika interakcji dla klasy Prezes_dodaj_pojazd.xaml
    /// </summary>
    public partial class Prezes_dodaj_pojazd : Window
    {
        public Prezes_dodaj_pojazd()
        {
            InitializeComponent();
        }



        private bool CheckPoprawnoscdanych()
        {

            if (String.IsNullOrEmpty(model.Text)) return false;
            if (String.IsNullOrEmpty(rok.Text)) return false;
            if (String.IsNullOrEmpty(vin.Text)) return false;
            if (String.IsNullOrEmpty(przebieg.Text)) return false;
            if (String.IsNullOrEmpty(numer_dowodu.Text)) return false;
            if (String.IsNullOrEmpty(numer_rejestracyjny.Text)) return false;
            if (String.IsNullOrEmpty(numer_ubezpieczenia.Text)) return false;
            if (String.IsNullOrEmpty(segment.Text)) return false;
            return true;
        }

        string DatePickertoMySQLdate(string DatePicker)
        {
            return DatePicker.Substring(6, 4) + "-" + DatePicker.Substring(3, 2) + "-" + DatePicker.Substring(0, 2) + DatePicker.Substring(10, 9); ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckPoprawnoscdanych())
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=andrea;PASSWORD=zaq1@WSX");
                conn.Open();

                string exist = $"select count(pojazd.pojazd_id) from tesla.pojazd where pojazd.nr_vin='{vin.Text}'";

                MySqlCommand comm = new MySqlCommand(exist, conn);

                string czy_istnieje = comm.ExecuteScalar().ToString();

                if (System.Convert.ToInt32(czy_istnieje) == 1)
                {
                    MessageBox.Show("UWAGA!! W bazie już Pojazd o podanym numerze VIN");
                }
                else
                {
                    string additionalInfoAboutCar=txtBoxAdditionalInfo.Text;
                    if (String.IsNullOrEmpty(additionalInfoAboutCar)) {
                        additionalInfoAboutCar = "0";
                    }
                    string New_car = $"insert into `tesla`.`pojazd` (model,rok_produkcji,nr_vin,przebieg,nr_dowodu_rej,nr_rejestracyjny,id_ubezpieczenia1,segment_pojazdu,ubez_od,ubez_dp,Dodatkowe_Informacje) VALUES('{model.Text}','{DatePickertoMySQLdate(rok.ToString())}','{vin.Text}','{przebieg.Text}','{numer_dowodu.Text}','{numer_rejestracyjny.Text}','{numer_ubezpieczenia.Text}','{segment.Text}','{DatePickertoMySQLdate(Ubezpieczenie_od.ToString())}','{DatePickertoMySQLdate(Ubezpieczenie_do.ToString())}','{additionalInfoAboutCar}')";

                    comm = new MySqlCommand(New_car, conn);

                    var a = comm.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Pojazd dodany.");
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
