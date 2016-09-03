using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EODG.ElementaryEncryption.Ui
{
    public class MainViewModel
    {
        private EncryptionData _encryptionData;

        public MainViewModel()
        {
            _encryptionData = new EncryptionData();
        }

        public EncryptionData EncryptionData
        {
            get { return _encryptionData; }
        }

        public void Encrypt()
        {

        }

        public void LoadedEncrypt()
        {

        }

        public void Decrypt()
        {

        }
    }
}
