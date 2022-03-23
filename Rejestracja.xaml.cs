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
    /// Logika interakcji dla klasy Rejestracja.xaml
    /// </summary>
    public partial class Rejestracja : Window
    {
        public Rejestracja()
        {
            InitializeComponent();
            lblModel.Visibility = Visibility.Hidden;
            lstboxModele.Visibility = Visibility.Hidden;
        }
        bool CheckPoprawnoscdanych() {
            if (!timeWypozyczenie_OD.SelectedDate.HasValue)      return false;
            if (!timeWypozyczenie_DO.SelectedDate.HasValue)      return false;
            if (lstboxModele.SelectedIndex<0)                    return false;
            if (!checkboxOswiadczenie1.IsChecked.Value)          return false;
            if (!checkboxOswiadczenie2.IsChecked.Value)          return false;
            if (String.IsNullOrEmpty(txtImie.Text))              return false;
            if (String.IsNullOrEmpty(txtNazwisko.Text))          return false;
            if (String.IsNullOrEmpty(txtNrDowoduOs.Text))        return false;
            if (String.IsNullOrEmpty(txtNrPrawaJazdy.Text))      return false;
            if (String.IsNullOrEmpty(txtTelefon.Text))           return false;
            if (timeWypozyczenie_OD.SelectedDate>timeWypozyczenie_DO.SelectedDate || timeWypozyczenie_DO.SelectedDate < timeWypozyczenie_OD.SelectedDate) return false;

            return true;
        }


        private void btnFINALRezerwacja_Click(object sender, RoutedEventArgs e)
        {
            bool czyPrzeslacFormularz = CheckPoprawnoscdanych();
            if (czyPrzeslacFormularz == true) {
                string Imie=txtImie.Text;
                string Nazwisko=txtNazwisko.Text;
                string NrDowoduOsobistego = txtNrDowoduOs.Text;
                string NrPrawaJazdy = txtNrPrawaJazdy.Text;
                string telefon = txtTelefon.Text;
                string haselko = pswd.Password.ToString();
                string[] tablicaAutDostepnych = getTablicaAut();


                if (!CzyKlientIstnieje(NrDowoduOsobistego))
                {
                    WprowadzNowegoKlienta(Nazwisko,Imie,NrDowoduOsobistego,telefon,NrPrawaJazdy,haselko);
                }

                WprowadzNowyWynajem(ZnajdzKlientaPoDowodzie(NrDowoduOsobistego), timeWypozyczenie_OD.ToString(), timeWypozyczenie_DO.ToString(), "1", tablicaAutDostepnych[this.lstboxModele.SelectedIndex]);

                MessageBox.Show("Przesłano rezerwację!");
                Close();
            }

        }
        bool CzyKlientIstnieje(string NrDowoduOsobistego) {
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");
            conn.Open();
            string SprawdzenieKlienta = $"Select nr_dowodu_os from tesla.klient;";
            MySqlCommand dowodyOs = new MySqlCommand(SprawdzenieKlienta, conn);

            MySqlDataReader rdr = dowodyOs.ExecuteReader();
            
            while (rdr.Read())
            {
                if (rdr.GetString(0) == NrDowoduOsobistego)
                {
                    Close();
                    return true;
                }
            }
            Close();
            return false;
        }
        void WprowadzNowegoKlienta(string Nazwisko, string Imie, string NrDowoduOsobistego,string telefon, string NrPrawaJazdy, string haslo) {
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");
            conn.Open();
            string insertWypozyczenie = $"Insert into tesla.klient (nazwisko,imie,nr_dowodu_os,nr_tel,nr_prawa_jazdy,haslo) Values ('{Nazwisko}', '{Imie}','{NrDowoduOsobistego}','{telefon}','{NrPrawaJazdy}','{haslo}');";
            MySqlCommand wprowadzenie = new MySqlCommand(insertWypozyczenie, conn);
            wprowadzenie.ExecuteNonQuery();
            Close();
        }
        void WprowadzNowyWynajem(string idKlienta, string Wypozyczenieod, string Wypozyczeniedo, string idPracownika,string idPojazdu) {
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");
            conn.Open();
            string insertWypozyczenie = $"Insert into tesla.wynajem (id_klienta,data_od,data_do,id_prac_obslugujacego,pojazd_id) Values ('{idKlienta}', '{DatePickertoMySQLdate(Wypozyczenieod)}','{DatePickertoMySQLdate(Wypozyczeniedo)}','{idPracownika}','{idPojazdu}');";
            MySqlCommand wprowadzenie = new MySqlCommand(insertWypozyczenie, conn);
            wprowadzenie.ExecuteNonQuery();
            Close();
        }
        string  ZnajdzKlientaPoDowodzie(string NrDowoduOsobistego) {
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");
            conn.Open();
            string id = $"Select id_klienta from tesla.klient where nr_dowodu_os like '{NrDowoduOsobistego}' ;";
            MySqlCommand idOsoby = new MySqlCommand(id, conn);
            string zwracany = idOsoby.ExecuteScalar().ToString();
            conn.Close();
            return zwracany;
        }
        string DatePickertoMySQLdate(string DatePicker) {
            return DatePicker.Substring(6, 4) + "-" + DatePicker.Substring(3, 2) + "-" + DatePicker.Substring(0, 2) + DatePicker.Substring(10, 9); ;
        }
        int wypelnienieAutiSuma(string wypozyczenieOd, string wypozyczenieDo)
        {
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");
            conn.Open();

            String query2 = $"SELECT distinct * from pojazd ,wynajem WHERE '{DatePickertoMySQLdate(timeWypozyczenie_DO.ToString())}' < wynajem.data_od OR '{DatePickertoMySQLdate(timeWypozyczenie_OD.ToString())}' > wynajem.data_do";
            MySqlCommand modeledostepne = new MySqlCommand(query2, conn);

            MySqlDataReader rdr = modeledostepne.ExecuteReader();
            int sumaDostepnychAut=0;
            while (rdr.Read())
            {
                if (rdr.GetString(11) != "0")
                {
                    lstboxModele.Items.Add($"Model: {rdr.GetString(1)} {rdr.GetString(11)}\nRok produkcji: {rdr.GetString(2).Substring(6, 4)}\nSegment pojazdu: {rdr.GetString(10)}");
                }
                else lstboxModele.Items.Add($"Model: {rdr.GetString(1)}\nRok produkcji: {rdr.GetString(2).Substring(6, 4)}\nSegment pojazdu: {rdr.GetString(10)}");
                sumaDostepnychAut += 1;
            }
            conn.Close();
            return sumaDostepnychAut;
        }
        int wypelnienieAutiSuma()
        {
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");
            conn.Open();

            String query2 = $"SELECT distinct * from pojazd ,wynajem WHERE '{DatePickertoMySQLdate(timeWypozyczenie_DO.ToString())}' < wynajem.data_od OR '{DatePickertoMySQLdate(timeWypozyczenie_OD.ToString())}' > wynajem.data_do";
            MySqlCommand modeledostepne = new MySqlCommand(query2, conn);

            MySqlDataReader rdr = modeledostepne.ExecuteReader();
            int sumaDostepnychAut = 0;
            while (rdr.Read())
            {
                sumaDostepnychAut += 1;
            }
            conn.Close();
            return sumaDostepnychAut;
        }
        string[] getTablicaAut() {
            string[] tablica = new string[wypelnienieAutiSuma()];
            int pomocnicza = 0;
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");
            conn.Open();

            String query2 = $"SELECT distinct * from pojazd ,wynajem WHERE '{DatePickertoMySQLdate(timeWypozyczenie_DO.ToString())}' < wynajem.data_od OR '{DatePickertoMySQLdate(timeWypozyczenie_OD.ToString())}' > wynajem.data_do";
            MySqlCommand modeledostepne = new MySqlCommand(query2, conn);

            MySqlDataReader rdr = modeledostepne.ExecuteReader();
            while (rdr.Read())
            {
                tablica[pomocnicza++]= rdr.GetString(0);//dodawaie do tablicy kolejno ID aut z listboxa
            }
            conn.Close();
            return tablica;
        }


        private void btnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnWczytajAuta_Click(object sender, RoutedEventArgs e)
        {
            lstboxModele.Items.Clear();
            if (!timeWypozyczenie_OD.SelectedDate.HasValue || !timeWypozyczenie_DO.SelectedDate.HasValue || timeWypozyczenie_OD.SelectedDate > timeWypozyczenie_DO.SelectedDate || timeWypozyczenie_DO.SelectedDate < timeWypozyczenie_OD.SelectedDate)
            {
                MessageBox.Show("Wprowadź prawidłową datę wynajmu");
            }
            else {
                wypelnienieAutiSuma(timeWypozyczenie_OD.ToString(),timeWypozyczenie_DO.ToString());
                lblModel.Visibility = Visibility.Visible;
                lstboxModele.Visibility = Visibility.Visible;
            }

        }
    }
}
