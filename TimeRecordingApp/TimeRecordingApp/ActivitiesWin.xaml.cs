using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeRecordingApp.Cmds;
using TimeRecordingApp.ViewModels;

namespace TimeRecordingApp
{
    /// <summary>
    /// Interaktionslogik für Activities.xaml
    /// </summary>
    public partial class ActivitiesWin : Window
    {

        public MainWindowViewModel ViewModel { get; set; }

        public ActivitiesWin(MainWindowViewModel theViewModel)
        {
            ViewModel = theViewModel;
            //MainWindow mainWin = Application.Current.MainWindow as MainWindow;

            ViewModel.AddActivityRecordCmd = new AddActivityRecordCommand(this);

            InitializeComponent();
            //mGridActivities.ItemsSource = mainWin.ViewModel.ActivityRecords;




        }
    }
}
