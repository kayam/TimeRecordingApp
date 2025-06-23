using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using TimeRecording.Dal.Entities;
using TimeRecordingApp.ViewModels;

namespace TimeRecordingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindowViewModel ViewModel { get; set; }

        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();


        protected override void OnClosing(CancelEventArgs e)
        {
            if (ViewModel.IsDirty)
            {
                MessageBoxResult result = MessageBox.Show("Es gibt ungespeicherte Änderungen. Wollen Sie das Programm wirklich beenden?", "Ungespeicherte Änderungen", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true; // Abbrechen des Schließvorgangs
                    return;
                }
            }
                base.OnClosing(e);
            // Optional: Ask the user for confirmation
            // Application.Current.Shutdown(); // optional, not needed here
        }

        public MainWindow() {
            ViewModel = new MainWindowViewModel(this);
            InitializeComponent();

            // ViewModel.AddTimeRecordCmd.Execute(gridTimeRecords.ItemsSource);
            
            ICollectionView view = CollectionViewSource.GetDefaultView (gridTimeRecords.ItemsSource);
            view.SortDescriptions.Add (new SortDescription("Date", ListSortDirection.Ascending));
            view.SortDescriptions.Add (new SortDescription("StartTime", ListSortDirection.Ascending));

            view.Refresh();

            if (gridTimeRecords.Items.Count > 0) {
                gridTimeRecords.ScrollIntoView(gridTimeRecords.Items.GetItemAt(gridTimeRecords.Items.Count - 1));
               // var border = VisualTreeHelper.GetChild(gridTimeRecords, 0);
              // if (border != null) {
                   // var scroll = border.Child as ScrollViewer;
                   // if (scroll != null) scroll.ScrollToEnd();
               // }
            }

            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            //System.Windows.Markup.StaticExtension


            //lblWeather.Content =  WeatherData ().Result;
        }

        private async Task <string> WeatherData () {

            double latitude = 51.18;    // Remscheid lat
            double longitude = 7.20;    // Remscheid lon
            string url = $"https://api.open-meteo.com/v1/forecast?" +
                         $"latitude={latitude}&longitude={longitude}" +
                         "&current_weather=true&timezone=Europe/Berlin";

            using HttpClient client = new HttpClient();
            try {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);
                JsonElement cw = doc.RootElement[0].GetProperty("current_weather");

                double temp = cw.GetProperty("temperature").GetDouble();
                double wind = cw.GetProperty("windspeed").GetDouble();
                string condition = GetWeatherDescription ( int.Parse(cw.GetProperty("weathercode").GetRawText()));

                string ret;

                ret = $"Remscheid: {temp} °C  {condition}";
                ret += $"\nWind: {wind} m/s";
                return ret;
            } catch (Exception ex) {

                return $"Error: {ex.Message}";
            }
        }


    string GetWeatherDescription(int code)
    {
        return code switch
        {
            0 => "Klarer Himmel",
            1 => "Überwiegend klar",
            2 => "Teilweise bewölkt",
            3 => "Bewölkt",
            45 => "Nebel",
            48 => "Reifiger Nebel",
            51 or 53 or 55 => "Nieselregen",
            61 or 63 or 65 => "Regen",
            71 or 73 or 75 => "Schnee",
            80 or 81 or 82 => "Regenschauer",
            95 => "Gewitter",
            96 or 99 => "Gewitter mit Hagel",
            _ => "Unbekannt"
        };
    }


        private void Timer_Click(object sender, EventArgs e) {
            DateTime d;
            d = DateTime.Now;
            lblTime.Content = WithZero (d.Hour) + ":" + WithZero(d.Minute) + ":" + WithZero (d.Second);
        }

        private string WithZero (int theTime)
        {
            if (theTime < 10) {
                return "0" + theTime;
            } else {
                return theTime.ToString ();
            }

        }

        private void DataGridTextColumn_Error (object sender, ValidationErrorEventArgs e) {
            if (e.Action == ValidationErrorEventAction.Added) {
                MessageBox.Show (e.Error.ErrorContent.ToString());
            }
        }

        /*
        private void gridTimeRecords_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            gridTimeRecords.ScrollIntoView(e.NewItem);
        } */

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ViewModel.IsDirty) {
                e.CanExecute = false;
            } else { 
                e.CanExecute = true;
            }
        }

        private void MenuItemActivities_Click(object sender, RoutedEventArgs e)
        {
            
            ActivitiesWin win = new ActivitiesWin (ViewModel);
            win.Owner = this;
            win.ShowDialog ();

        }

        private bool mOnlyToday = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //CollectionViewSource cvs = (CollectionViewSource)Resources["FilteredTimeRecords"];
            mOnlyToday = !mOnlyToday;
            ICollectionView view = CollectionViewSource.GetDefaultView(gridTimeRecords.ItemsSource);
            view.Refresh();
            mTodayButton.Content = mOnlyToday ? "Alle" : "Heute";
        }


        private void FilteredTimeRecords_Filter (object sender, FilterEventArgs e)
        {
            if (!mOnlyToday)
            {
                e.Accepted = true;
                return;
            }
            if (e.Item is TimeRecordings rec)
            {
                DateTime today = DateTime.Today;


                e.Accepted = rec.Date.Date == today.Date;
                             //rec.Name.ToLower().Contains(_filterText);
            }
        }

        private async void MainWin_Loaded (object sender, RoutedEventArgs e) {
            lblWeather.Content = await WeatherData ();
        }
    }
}
