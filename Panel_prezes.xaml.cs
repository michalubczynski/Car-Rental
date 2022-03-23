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

    public partial class Panel_prezes : Window
    {
        public Panel_prezes()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Prezes_aktualne_wypozyczenia();

            window.Owner = this;
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = new Prezes_dodaj_pojazd();

            window.Owner = this;
            window.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new Prezes_usun_pojazd();

            window.Owner = this;
            window.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var window = new Prezes_nowy_pracownik();

            window.Owner = this;
            window.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var window = new Prezes_usun_pracownika();

            window.Owner = this;
            window.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var window = new Prezes_edytuj_pracownika();

            window.Owner = this;
            window.Show();
        }
    }
}
