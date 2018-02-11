using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App62.Model
{
    public class Payment
    {

        private long creditcardNumber;
        private DateTime paidDate;
        private decimal total;

        public Payment(long cardNumber, decimal totalprice)
        {
            creditcardNumber = cardNumber;
            total = totalprice;
            paidDate = DateTime.Now;
        }
    }
}
