using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TimeRecording.Dal.Entities;

namespace TimeRecordingApp.Cmds
{
    public class DeleteTimeRecordCommand : CommandBase
    {
        public override bool CanExecute(object parameter) =>
            parameter is DataGrid dataGrid && dataGrid.SelectedItem != null;
        

        public override void Execute(object parameter)
        {
            if ( ! (parameter is DataGrid dataGrid)) return;

            if (dataGrid.SelectedItem != null)
            {
                if (dataGrid.ItemsSource is ObservableCollection<TimeRecordings> col)
                {
                    ((TimeRecordings)dataGrid.SelectedItem).MIsDeleted = true;
                    ((TimeRecordings)dataGrid.SelectedItem).MIsChanged = true;
                }
                    
            }

        }

    }
}
