using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelsProject
{
   public class RegisterClient
    {
        private int _id;

        public int RegisterID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _deviceName;

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }
        private string _registerName;

        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }
        
       
        
        
    }
}
