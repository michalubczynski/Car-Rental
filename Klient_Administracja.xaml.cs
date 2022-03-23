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

    public partial class Klient_Administracja : Window
    {
        public Klient_Administracja(string login)
        {
            InitializeComponent();

            if (String.IsNullOrEmpty(login)) MessageBox.Show("Wystąpił poważny błąd... Skontaktuj się z Administratorem");
            else
            {
                MySqlConnection conn = new MySqlConnection("SERVER=localhost;Database=tesla;Integrated Security=True;UID=michal;PASSWORD=zaq1@WSX");

                var ClientIdQuery = $"select id_klienta from klient where nr_dowodu_os='{login}'";

                MySqlCommand comm = new MySqlCommand(ClientIdQuery, conn);
                conn.Open();
                var id_klienta = comm.ExecuteScalar();
                String query = $"select wynajem.data_od,wynajem.data_do,pojazd.nr_rejestracyjny,pojazd.model,klient.imie,klient.nazwisko,klient.nr_tel from klient,pojazd,wynajem where wynajem.id_klienta=klient.id_klienta and wynajem.pojazd_id=pojazd.pojazd_id and wynajem.id_klienta='{id_klienta.ToString()}'";
                comm = new MySqlCommand(query, conn);

               DataTable dt = new DataTable();

               dt.Load(comm.ExecuteReader());
                dtgrid.DataContext = dt;
                conn.Close();
            }




        }
    }
}
