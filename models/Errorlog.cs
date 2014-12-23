using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace models
{
  public  class Errorlog
    {
        private int _registerId;

        public int RegisterId
        {
            get { return _registerId; }
            set { _registerId = value; }
        }

        private DateTime _timestamp;

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _stacktrace;

        public string Stacktrace
        {
            get { return _stacktrace; }
            set { _stacktrace = value; }
        }
        
        
    }
}
