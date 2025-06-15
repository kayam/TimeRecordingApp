using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecordingApp.ViewModels;

namespace TimeRecordingApp.Cmds
{
    public class SaveActivityRecordCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => parameter is MainWindowViewModel m && m.IsDirtyActivities == true;

        public override void Execute(object parameter)
        {
            if (!(parameter is MainWindowViewModel m)) return;
            m.SaveActivities ();
        }



    }
}
