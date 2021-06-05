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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Data;

namespace MeetingOrganizer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindData();
            BindPersonData();
        }
        private void CreateMeetingBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateMeetingWindow cmw = new CreateMeetingWindow();
            cmw.ShowDialog();
            BindData();

        }
        private void DelateMeetingBtn_Click(object sender, RoutedEventArgs e)
        {
            DelateMeeting();
        }

        public void BindData()
        {
            MeetingsDataGrid.ItemsSource = DataBaseSetting.pokazSpotkania();
        }

        public void BindPersonData()
        {
            PersonsDataGrid.ItemsSource = DataBaseSetting.pokazOsoby();
        }

        public void DelateMeeting()
        {
            object item = MeetingsDataGrid.SelectedItem;
            string id = (MeetingsDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            DataBaseSetting.UsunSpotkanie(new Spotkanie { IdSpotkania = Convert.ToInt32(id)});
            BindData();
        }

        private void MeetingListBtn_Click(object sender, RoutedEventArgs e)
        {
            MeetingsDataGrid.Visibility = Visibility.Visible;
            CreateMeetingBtn.Visibility = Visibility.Visible;
            DelateMeetingBtn.Visibility = Visibility.Visible;
            MeetingsDataGridInfo.Visibility = Visibility.Visible;

            PersonsDataGrid.Visibility = Visibility.Hidden;
            AddPersonBtn.Visibility = Visibility.Hidden;
            DelatePersonBtn.Visibility = Visibility.Hidden;
            PersonsDataGridInfo.Visibility = Visibility.Hidden;
            BindPersonData();

        }

        private void PersonsListBtn_Click(object sender, RoutedEventArgs e)
        {
            MeetingsDataGrid.Visibility = Visibility.Hidden;
            CreateMeetingBtn.Visibility = Visibility.Hidden;
            DelateMeetingBtn.Visibility = Visibility.Hidden;
            MeetingsDataGridInfo.Visibility = Visibility.Hidden;

            PersonsDataGrid.Visibility = Visibility.Visible;
            AddPersonBtn.Visibility = Visibility.Visible;
            DelatePersonBtn.Visibility = Visibility.Visible;
            PersonsDataGridInfo.Visibility = Visibility.Visible;
            BindPersonData();
        }

        private void AddPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow apw = new AddPersonWindow();
            apw.ShowDialog();
            BindPersonData();
        }

        private void DelatePersonBtn_Click(object sender, RoutedEventArgs e)
        {
            object item = PersonsDataGrid.SelectedItem;
            string id = (PersonsDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            DataBaseSetting.UsunOsobe(new Osoba { IdOsoby = Convert.ToInt32(id) });
            BindPersonData();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            object item = MeetingsDataGrid.SelectedItem;
            string id = (MeetingsDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

            MeetingWindow.MeetingId = Convert.ToInt32(id);
            MeetingWindow mw = new MeetingWindow();
            mw.ShowDialog();
        }
    }
}
