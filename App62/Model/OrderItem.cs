using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using App62.Annotations;

namespace App62.Model
{
    public class OrderItem : INotifyPropertyChanged
    {
        private Pizza _pizzaItem;
        private int _quantity;

        public Pizza PizzaItem
        {
            get
            {
                return _pizzaItem;
            }

            set
            {
                _pizzaItem = value;
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged("ItemPrice");
            }
        }

        public decimal ItemPrice
        {
            get { return CalculateOrderItemPrice(); } 
            
        }

        public OrderItem(Pizza pizza)
        {
            _pizzaItem = pizza;
            _quantity = 1;
        }

        public OrderItem(Pizza pizza, int quantity)
        {
            _pizzaItem = pizza;
            _quantity = quantity;
        }
        public decimal CalculateOrderItemPrice()
        {
            return _pizzaItem.Price*_quantity;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
