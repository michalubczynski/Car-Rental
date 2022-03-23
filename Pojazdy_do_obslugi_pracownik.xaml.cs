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

    public partial class Pojazdy_do_obslugi_pracownik : Window
    {
        public Pojazdy_do_obslugi_pracownik(string login)
        {
            InitializeComponent();
            MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");

            var WorkerIdQuery = $"select pracownik_id from pracownik where nr_dowodu_os='{login}'";

            MySqlCommand comm = new MySqlCommand(WorkerIdQuery, conn);
            conn.Open();

            var id_prac = comm.ExecuteScalar();

            String query = $"select wynajem.data_od,wynajem.data_do,pojazd.nr_rejestracyjny,pojazd.model,klient.imie,klient.nazwisko,klient.nr_tel from klient,pojazd,wynajem where wynajem.id_klienta=klient.id_klienta and wynajem.pojazd_id=pojazd.pojazd_id and wynajem.id_prac_obslugujacego='{id_prac.ToString()}' and data_do>curdate()";
            comm = new MySqlCommand(query, conn);

            DataTable dt = new DataTable();

            dt.Load(comm.ExecuteReader());
            dtgrid.DataContext = dt;
            conn.Close();
        }

    }
}
