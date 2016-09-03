using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EODG.ElementaryEncryption.Ui
{
    public class EncryptionData : INotifyPropertyChanged
    {
        #region FIELDS
        private string _cipher;
        private string _initializatoinVector;
        private string _key;
        #endregion

        #region CONSTRUCTOR
        public EncryptionData() { }
        #endregion

        #region EVENTS
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region PROPERTIES

        public string Cipher
        {
            get { return _cipher; }
            set
            {
                _cipher = value;
                OnPropertyChanged("Cipher");
            }
        }

        public string InitializationVector
        {
            get { return _initializatoinVector; }
            set
            {
                _initializatoinVector = value;
                OnPropertyChanged("InitializationVector");
            }
        }

        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged("Key");
            }
        }

        #endregion

        #region PRIVATE METHODS
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
