using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace models
{
  public  class Product
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        //public override string ToString()
        //{
        //    return ProductName + "  €" + Price;
        //}
        
    }

    
}
