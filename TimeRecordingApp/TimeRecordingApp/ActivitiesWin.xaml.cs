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

namespace TimeRecordingApp
{
    /// <summary>
    /// Interaktionslogik für Activities.xaml
    /// </summary>
    public partial class ActivitiesWin : Window
    {

        public ActivitiesWin()
        {

            MainWindow mainWin = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
            mDataGrid.ItemsSource = mainWin.ViewModel.ActivityRecords;

        }
    }
}
