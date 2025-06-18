using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeRecording.Dal.Entities;

namespace TimeRecordingApp.Cmds
{
    public class AddActivityRecordCommand : CommandBase
    {
        public ActivitiesWin MActivitiesWin { get; set; }

        public AddActivityRecordCommand(ActivitiesWin theWin)
        {
            MActivitiesWin = theWin;
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is ObservableCollection<Activities>;
        }

        public override void Execute(object parameter)
        {
            if (!(parameter is ObservableCollection<Activities> activitiyRecords)) return;
            //var maxCount = timeRecords.Max(x => x.Id);
            var newItem = new Activities { Description = "", Priority = 0, MIsChanged = true };
            activitiyRecords.Add (newItem);

            MActivitiesWin.mGridActivities.Focus();
            DataGridCellInfo cellInfo = new DataGridCellInfo(newItem, MActivitiesWin.mGridActivities.Columns[0]);
            MActivitiesWin.mGridActivities.CurrentCell = cellInfo;
            MActivitiesWin.mGridActivities.ScrollIntoView(newItem);
            MActivitiesWin.mGridActivities.BeginEdit();
            MActivitiesWin.mGridActivities.SelectedIndex = -1;
        }
    }
}
