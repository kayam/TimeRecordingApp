using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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



        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                DataObject.RemovePastingHandler(tb, OnPaste); // doppelte Registrierung vermeiden
                DataObject.AddPastingHandler(tb, OnPaste);
            }
        }

        private bool IsTextNumeric(string text)
        {
            return Regex.IsMatch(text, @"^\d+$");
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = e.DataObject.GetData(typeof(string)) as string;
                if (!IsTextNumeric(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


    }
}
