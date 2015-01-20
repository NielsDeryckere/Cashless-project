using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace models
{
    public class Employee:IDataErrorInfo

    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
       

        //private string _employeeName;
        [Required(ErrorMessage = "scan your identitycard")]
        public string EmployeeName
        {
            get;
            set;
        }

        //private string _address;
          [Required(ErrorMessage = "scan your identitycard")]
          public string Address
          {
              get;
              set;
          }

        //private string _email;
          [Required(ErrorMessage = "Please fill in an emailaddress")]
          [EmailAddress(ErrorMessage = "The submitted emailaddress is not valid")]
          public string Email
          {
              get;
              set;
          }

        //private string _phone;
          [Required(ErrorMessage = "Please fill in a phonenumber")]
          [StringLength(50, MinimumLength = 9)]
          public string Phone
          {
              get;
              set;
          }

        private long _barcode;

        public long Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        public bool IsValid()
        {

            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);



        }


        public string Error
        {
            get { return null; }
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
