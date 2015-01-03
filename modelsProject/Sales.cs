using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace models
{
   public class Sales
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _timestamp;

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private long _customerId;

        public long CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private int _registerId;

        public int RegisterId
        {
            get { return _registerId; }
            set { _registerId = value; }
        }

        private int _productId;

        public int ProductId 
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private double _totalPrice;

        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        
        
    }
}
