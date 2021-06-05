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
    /// Logika interakcji dla klasy AddPersonToMeetingWindow.xaml
    /// </summary>
    public partial class AddPersonToMeetingWindow : Window
    {
        
        public AddPersonToMeetingWindow()
        {
            InitializeComponent();
            BindPersonData();
        }
        public void BindPersonData()
        {
            PersonsDataGrid.ItemsSource = DataBaseSetting.pokazOsoby();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            object item = PersonsDataGrid.SelectedItem;
            string id = (PersonsDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            MeetingWindow.PersonId = Convert.ToInt32(id);
            
            this.Close();
        }

        private void AddNewPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow apw = new AddPersonWindow();
            apw.ShowDialog();
            BindPersonData();
        }
    }
}
