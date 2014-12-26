using System;
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

        [Required(ErrorMessage = "scan aub uw identiteitskaart in")]
        [StringLength(50, MinimumLength = 2)]
        public string CustomerName
        {
            get;
            set;
            //get { return _customerName; }
            //set { _customerName = value; }
        }

       // private string _address;
        [Required(ErrorMessage = "scan aub uw identiteitskaart in")]
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
      
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        private string _barCode;
       
        public string Barcode
        {
            get { return _barCode; }
            set { _barCode = value; }
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
