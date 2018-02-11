using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App62.Model
{
    public class Payments
    {
        private List<Payment> paymentsRecord;

        public Payments()
        {
            paymentsRecord = new List<Payment>();
        }

        public void payInvoice(Payment p)
        {
            paymentsRecord.Add(p);
        }
    }
}
