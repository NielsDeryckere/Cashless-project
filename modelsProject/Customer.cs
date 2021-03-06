﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace models
{
    public class Customer:IDataErrorInfo
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
       
       // private string _customerName;

        [Required(ErrorMessage = "scan your identitycard")]
        [StringLength(50, MinimumLength = 2)]
        public string CustomerName
        {
            get;
            set;
            //get { return _customerName; }
            //set { _customerName = value; }
        }

       // private string _address;
        [Required(ErrorMessage = "scan your identitycard")]
        [StringLength(50, MinimumLength = 2)]
        public string Address
        {
            get;
            set;
            //get { return _address; }
            //set { _address = value; }
        }

        private byte[] _picture;
        
      
        public byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }//het path waar de afbeelding zich bevindt

        private double _balance;

      [Range(0.00, 999999999,ErrorMessage="Please fill in a valid value")]
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        private long _barCode;
       [Required(ErrorMessage = "scan your identitycard")]
        public long Barcode
        {
            get { return _barCode; }
            set { _barCode = value; }
        }

       private bool _active;

       public bool Active
       {
           get { return _active; }
           set { _active = value; }
       }
       

        public string Error
        {
            get { return null; }
          
        }

        public bool IsValid()
        {
            
                return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
            

            
        }
        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }
    }
}
