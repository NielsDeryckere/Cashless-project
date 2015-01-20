using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace models
{
  public  class Product:IDataErrorInfo
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

       // private string _productName;
        [Required(ErrorMessage = "Fill in a productname")]
        [StringLength(50, MinimumLength = 3)]
        public string ProductName
        {
            get;
            set;
        }

        //private double _price;
        [Required(ErrorMessage = "Fill in a price")]
        [Range(0.00, 999999999, ErrorMessage = "Please fill in a valid value")]
        public double Price
        {
            get;
            set;
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
        //public override string ToString()
        //{
        //    return ProductName + "  €" + Price;
        //}


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
