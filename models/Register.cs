using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace models
{
  public class Register
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _registerName;

        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }

        private string _deviceName;

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        private DateTime _purchaseDate;

        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }

        private DateTime _expiresDate;

        public DateTime ExpiresDate
        {
            get { return _expiresDate; }
            set { _expiresDate = value; }
        }


        
        
    }
}
