using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    }
}
