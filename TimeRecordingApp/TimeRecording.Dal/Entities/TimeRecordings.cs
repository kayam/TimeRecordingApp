using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeRecording.Dal.Entities
{
    public partial class TimeRecordings : BaseEntity
    {

        #region Fields
        private int mId;
        private int? mActivityId;
        private DateTime mDate;
        private TimeSpan? mStartTime;
        private TimeSpan? mEndTime;
        private string   mNote;

        public int? ActivityId { 
            get
            {
                return mActivityId;
            }
            set
            {
                if (value == mActivityId) return;
                mActivityId = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey(nameof(ActivityId))]
        [InverseProperty(nameof(Activities.TimeRecordings))]
        public virtual Activities Activity { get; set; }
        #endregion Fields



        [Key]
        public int Id
        {
            get => mId;
            set
            {
                if (value == mId) return;
                mId = value;
             //   OnPropertyChanged();
            }
        }

        [Column(TypeName = "date")]
        public DateTime Date
        {
            get => mDate;
            set
            {
                if (value == mDate) return;
                mDate = value;
                OnPropertyChanged();
            }
        }


        [Column(TypeName = "time(0)")]
        public TimeSpan? StartTime
        {
            get => mStartTime;
            set
            {
                if (value == mStartTime) return;
                mStartTime = value;
                OnPropertyChanged();
            }
        }




        [Column(TypeName = "time(0)")]
        public TimeSpan? EndTime
        {
            get => mEndTime;
            set
            {
                if (value == mEndTime) return;
                mEndTime = value;
                OnPropertyChanged();
            }
        }

        [StringLength(2000)]
        public string Note
        {
            get => mNote;
            set
            {
                if (value == mNote) return;
                mNote = value;
                OnPropertyChanged();
            }
        }




    }
}
