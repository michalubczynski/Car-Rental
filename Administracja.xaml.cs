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

namespace Baza
{

    public partial class Administracja : Window
    {
        public Administracja(string login)
        {
            InitializeComponent();
            WorkerLogin = login;
        }
        private string WorkerLogin;

        private void Edytuj_klienta_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new Edytuj_klienta();
            window.Owner = this;
            window.Show();
        }

        private void Usun_klienta_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new Delete_client();
            window.Owner = this;
            window.Show();
        }

        private void Dodaj_klienta_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new Nowy_klient();
            window.Owner = this;
            window.Show();
        }

        private void pojazdy_do_obslugi_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new Pojazdy_do_obslugi_pracownik(WorkerLogin);
            window.Owner = this;
            window.Show();
        }

        private void Zlec_przeglad_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new Zlec_przeglad();
            window.Owner = this;
            window.Show();
        }

        private void odbierz_z_przegladu_Click(object sender, RoutedEventArgs e)
        {
            var window = new przeglad_odbior();
            window.Owner = this;
            window.Show();
        }
    }
}
