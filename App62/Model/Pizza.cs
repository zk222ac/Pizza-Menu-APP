using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App62.Model
{
    public class Pizza
    {
        public string Type { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }

        public Pizza()
        {

        }


        public Pizza(string type, string size, decimal price)
        {
            Type = type;
            Size = size;
            Price = price;
        }

        public override string ToString()
        {
            return "Pizza Type: " + Type + ": Size: " + Size + ": Price: " + Price;
        }
    }
}
