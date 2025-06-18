using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using TimeRecording.Dal.Entities;

namespace TimeRecordingApp.Cmds
{
    public class AddTimeRecordCommand : CommandBase
    {

        public AddTimeRecordCommand(Activities theDefaultActivity)
        {
            MDefaultActivity = theDefaultActivity;
        }

        public MainWindow MMainWin { get; set; }

        public Activities MDefaultActivity { get; }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (parameter is ListCollectionView view && view.SourceCollection is ObservableCollection<TimeRecordings> timeRecords) { 
                var newItem = new TimeRecordings { ActivityId = MDefaultActivity?.Id, Date = DateTime.Today, StartTime = DateTime.Now.TimeOfDay, EndTime = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(1)), Note = "", MIsChanged = true };
                timeRecords.Add(newItem);
             
                MMainWin.gridTimeRecords.Focus();
                DataGridCellInfo cellInfo = new DataGridCellInfo(newItem, MMainWin.gridTimeRecords.Columns[0]);
                MMainWin.gridTimeRecords.CurrentCell = cellInfo;
                MMainWin.gridTimeRecords.ScrollIntoView (newItem);
                MMainWin.gridTimeRecords.BeginEdit();
                MMainWin.gridTimeRecords.SelectedIndex = -1;
            }
        }

    }
}
