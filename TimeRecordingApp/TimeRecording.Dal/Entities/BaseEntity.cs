using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace TimeRecording.Dal.Entities
{
    public abstract class BaseEntity : INotifyPropertyChanged
    {

        #region Fields
        private bool mIsChanged = false;
        private bool mIsDeleted = false;
        #endregion Fields

        #region Properties

        [NotMapped]
        public bool MIsChanged
        {
            get => mIsChanged;
            set
            {
                if (value == mIsChanged) return;
                mIsChanged = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        public bool MIsDeleted
        {
            get => mIsDeleted;
            set
            {
                if (value == mIsDeleted) return;
                mIsDeleted = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties

        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion events


        #region Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != nameof(MIsChanged))
            {
                MIsChanged = true;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion Methods

    }
}
