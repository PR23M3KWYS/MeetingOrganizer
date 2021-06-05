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
    /// Logika interakcji dla klasy CreateMeetingWindow.xaml
    /// </summary>
    public partial class CreateMeetingWindow : Window
    {
        public CreateMeetingWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DodajSpotkanie();
        }

        private void DodajSpotkanie()
        {
            try
            {
                DataBaseSetting.DodajSpotkanie(new Spotkanie
                {
                    NazwaSpotkania=NazwaSpotkaniaTxtBox.Text,
                    GodzinaRozpoczecia=DateTime.Parse(GodzinaRozpoczeciaTxtBox.Text),
                    GodzinaZakonczenia=DateTime.Parse(GodzinaZakonczeniaTxtBox.Text),
                    Miejsce=MiejsceTxtBox.Text,

                });
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+" Nieprawidłowe dane");
            }
        }
    }
}
