﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace models
{
    public class Employee

    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
       

        private string _employeeName;

        public string EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private long _barcode;

        public long Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }


      
    }
}
