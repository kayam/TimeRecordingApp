using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeRecording.Dal.Entities
{
    public partial class Activities : BaseEntity
    {
        public Activities()
        {
            TimeRecordings = new HashSet<TimeRecordings>();
        }

        #region Fields
        private int mId;
        private string mDescription;
        private int? mPriority;


        [InverseProperty("Activity")]
        public virtual ICollection<TimeRecordings> TimeRecordings { get; set; }
        #endregion Fields

        #region Properties
        [Key]
        public int Id //MId
        {
            get => mId;
            set
            {
                if (value == mId) return;
                mId = value;
                OnPropertyChanged();
            }
        }


        [Required]
        [StringLength(2000)]
        public string Description //MDescription
        {
            get => mDescription;
            set
            {
                if (value == mDescription) return;
                mDescription = value;
                OnPropertyChanged();
            }
        }

        public int? Priority //MPriority
        {
            get => mPriority;
            set
            {
                if (value == mPriority) return;
                mPriority = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties



    }
}
