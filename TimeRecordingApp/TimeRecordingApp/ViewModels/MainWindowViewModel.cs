using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using TimeRecording.Dal.EfStructures;
using TimeRecording.Dal.Entities;
using TimeRecordingApp.Cmds;


namespace TimeRecordingApp.ViewModels
{


    public class MainWindowViewModel
    {
        public IList <TimeRecordings> TimeRecords { get; } = new ObservableCollection <TimeRecordings>();
        public IList <Activities> ActivityRecords { get; } = new ObservableCollection <Activities>();
        //public IList<TimeRecordings> TimeRecords { get; } = new ObservableCollection<TimeRecordings>();

        // public ListCollectionView View { get; }


        public ApplicationDbContext Context { get; }

        private ICommand mAddTimeRecordCommand = null;
        public ICommand AddTimeRecordCmd
        {
            get => mAddTimeRecordCommand;
            set
            {
                if (value != mAddTimeRecordCommand)
                {
                    mAddTimeRecordCommand = value;
                }
            }
        }//=> mAddTimeRecordCommand ??= new AddTimeRecordCommand();

       /* private ICommand mDeleteTimeRecordCommand = null;
        public ICommand DeleteTimeRecordCmd => mDeleteTimeRecordCommand ??= new DeleteTimeRecordCommand(); */

        private ICommand mSaveTimeRecordCommand = null;
        public ICommand SaveTimeRecordCmd => mSaveTimeRecordCommand ??= new SaveTimeRecordCommand();

        public MainWindow MMainWin { get; set; }

        public MainWindowViewModel (MainWindow theMainWin)
        {
            MMainWin = theMainWin;

            Context = new ApplicationDbContext ();
            Context.TimeRecordings.Include(t => t.Activity).ToList().ForEach( //TimeRecords.Add);
                delegate (TimeRecordings t)
                {
                    t.MIsChanged = false;
                    TimeRecords.Add(t);
                });

            

            Context.Activities.ToList().ForEach(ActivityRecords.Add);

            /*
            View = (ListCollectionView)
            CollectionViewSource.GetDefaultView(TimeRecords);
            View.Filter = delegate (object item)
            {
                TimeRecordings timeRecord = (TimeRecordings)item;
                return (timeRecord.MIsDeleted == false);
            };*/



            /*
            ActivityRecords.Add(
                new Activities { Id = 1, Description = "Programmieren", Priority = 10, MIsChanged = false, MIsDeleted = false });
            ActivityRecords.Add(
                new Activities { Id = 2, Description = "Analysierung", Priority = 5, MIsChanged = false, MIsDeleted = false });
            ActivityRecords.Add(
                new Activities { Id = 3, Description = "Aufräumen", Priority = 2, MIsChanged = false, MIsDeleted = false }
                ) ;

            TimeRecords.Add(
                new TimeRecordings { Id = 1, Activity = ActivityRecords[0], Date = DateTime.Today, StartTime = new TimeSpan (12, 30, 0), EndTime = new TimeSpan (13, 15, 0), Note = "test1", MIsChanged = false });
            TimeRecords.Add(
                new TimeRecordings { Id = 2, Activity = ActivityRecords[1], Date = DateTime.Today.AddDays(1), StartTime = new TimeSpan(12, 20, 33), EndTime = new TimeSpan(13, 22, 43), Note = "test1", MIsChanged = false });

            
            */
            Activities defaultActivity = ActivityRecords.FirstOrDefault ();
            AddTimeRecordCmd = new AddTimeRecordCommand(defaultActivity);
            ((AddTimeRecordCommand)AddTimeRecordCmd).MMainWin = MMainWin;

            //AddTimeRecordCmd.Execute (TimeRecords);

        }


        public bool IsDirty
        {
            get
            {
                foreach (TimeRecordings rec in TimeRecords)
                {
                    if (rec.MIsChanged)
                        return true;
                }
                return false;
            }
        }

        public void Save ()
        {
            foreach (TimeRecordings rec in TimeRecords.ToList<TimeRecordings> ())
            {
                if (rec.MIsDeleted)
                {
                    if (rec.Id != 0)
                        Context.TimeRecordings.Remove(rec);
                    TimeRecords.Remove(rec);
                } else {
                    rec.MIsChanged = false;
                    if (rec.Id == 0) { 
                       Context.TimeRecordings.Add(rec);
                    }
                }
            }

            Context.SaveChanges();


            //View.Refresh();
        }
    }
}
