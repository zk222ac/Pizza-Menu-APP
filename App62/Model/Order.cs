using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using App62.Annotations;

namespace App62.Model
{
    public class Order : INotifyPropertyChanged
    {
        

        public ObservableCollection<OrderItem> OrderItemsList { get; set; }
        public decimal TotalPrice { get { return CalculateTotalOrderPrice(); }  }
        public Order()
        {

           OrderItemsList = new ObservableCollection<OrderItem>();
        }

        public void AddOrderItem(Pizza pizzaItem, int quantity)
        {
            OrderItem ordItm = new OrderItem(pizzaItem, quantity);
            OrderItemsList.Add(ordItm);
        }

        public bool AddOrderItem(OrderItem ordItem)
        {
            if (OrderItemsList.Count > 0)
            {


                foreach (var Item in OrderItemsList)
                {
                    if (Item.PizzaItem.ToString().Equals(ordItem.PizzaItem.ToString()))
                    {
                        return false;
                    }
                }
            }


            OrderItemsList.Add(ordItem);
            OnPropertyChanged("TotalPrice");
            return true;



        }

        public decimal CalculateTotalOrderPrice()
        {
            decimal total = 0;
            if (OrderItemsList!=null && OrderItemsList.Count>0)
            {
                for (int i = 0; i < OrderItemsList.Count; i++)
                {
                    total += OrderItemsList[i].CalculateOrderItemPrice();
                }
            }

            return total;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
