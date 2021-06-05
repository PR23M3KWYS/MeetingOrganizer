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
using System.Data.SqlClient;

namespace MeetingOrganizer
{
    /// <summary>
    /// Logika interakcji dla klasy MeetingWindow.xaml
    /// </summary>
    public partial class MeetingWindow : Window
    {
        public static int MeetingId;
        public static int PersonId;

        public MeetingWindow()
        {
            InitializeComponent();
            BindActualData();
        }
        private void AddToMeetingBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPersonToMeetingWindow aptmw = new AddPersonToMeetingWindow();
            aptmw.ShowDialog();
            DodajDoSpotkania();
            BindActualData();
            
        }
        private void BindActualData()
        {
            MeetingPersonsDataGrid.ItemsSource = DataBaseSetting.pokazAktualne(new Aktualne { IdSpotkania = MeetingId });
        }
        private void DodajDoSpotkania()
        {
            try
            {
                if (DataBaseSetting.IleOsob(new Aktualne { IdSpotkania=MeetingId}) >= 25)
                {
                    MessageBox.Show("Osiągnięto maksymalny limit uczestników spotkania");
                }
                else
                {
                    DataBaseSetting.DodajAktualne(new Aktualne
                    {
                        IdSpotkania = MeetingId,
                        IdOsoby = PersonId,

                    });
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie dodano żadnej osoby");
            }
        }

        private void DelateFromMeetingBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = MeetingPersonsDataGrid.SelectedItem;
                string id = (MeetingPersonsDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                DataBaseSetting.UsunZeSpotkania(new Aktualne { IdOsoby = Convert.ToInt32(id) });
                BindActualData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie wybrano żadnego zapisu");
            }
        }
    }
}
