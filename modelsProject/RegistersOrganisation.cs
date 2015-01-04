using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace models
{
    public class RegistersOrganisation
    {
        private int _registerid;

        public int RegisterId
        {
            get { return _registerid; }
            set { _registerid = value; }
        }

        private int _organisationid;

        public int Organisationid
        {
            get { return _organisationid; }
            set { _organisationid = value; }
        }
        

        private DateTime _from;

        public DateTime From
        {
            get { return _from; }
            set { _from = value; }
        }

        private DateTime _untill;

        public DateTime Untill
        {
            get { return _untill; }
            set { _untill = value; }
        }
        
        
    }
}
