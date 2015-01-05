using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelsProject
{
    public class EmployeeRegister
    {
        private int _regiserID;

        public int RegisterID
        {
            get { return _regiserID; }
            set { _regiserID = value; }
        }

        private int _employeeID;

        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
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
