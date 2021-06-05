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

namespace MeetingOrganizer
{
    /// <summary>
    /// Logika interakcji dla klasy AddPersonWindow.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(AdresEmailTxtBox.Text.ToString().Contains("@"))
            {
                DodajOsobe();
            }
            else
            {
                MessageBox.Show("Nieprawidłowy adres e-mail");
            }
        }
        private void DodajOsobe()
        {
            try
            {
                DataBaseSetting.DodajOsobe(new Osoba
                {
                    Imie=ImieTxtBox.Text,
                    Nazwisko=NazwiskoTxtBox.Text,
                    AdresEmail=AdresEmailTxtBox.Text,
                });
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Nieprawidłowe dane");
            }
        }
    }
}
