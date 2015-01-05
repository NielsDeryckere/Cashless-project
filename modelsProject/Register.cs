using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }

        private string _deviceName;
      [Required]
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }
      [Required]
      
        private DateTime _purchaseDate;

      [Required]
      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }

      
        private DateTime _expiresDate;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiresDate
        {
            get { return _expiresDate; }
            set { _expiresDate = value; }
        }

        private string _assigned;
     
        public string Assigned
        {
            get { return _assigned; }
            set { _assigned = value; }
        }
        


        
        
    }
}
