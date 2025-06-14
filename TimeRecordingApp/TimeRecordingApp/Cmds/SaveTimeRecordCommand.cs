using System;
using System.Collections.ObjectModel;
using System.Linq;
using TimeRecordingApp.ViewModels;

namespace TimeRecordingApp.Cmds
{
    public class SaveTimeRecordCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => parameter is MainWindowViewModel m && m.IsDirty == true;

        public override void Execute(object parameter)
        {
            if ( ! (parameter is MainWindowViewModel m)) return;
                m.Save();
        }

    }
}
