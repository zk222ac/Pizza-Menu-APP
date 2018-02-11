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
    public class Customer : INotifyPropertyChanged
    {
        private string _name;
        private string _address;
        private string _phoneNo;
        private Order _custOrder;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
        public string PhoneNo
        {
            get
            {
                return _phoneNo;
            }

            set
            {
                _phoneNo = value;
                OnPropertyChanged();
            }
        }
        public Order CustOrder {get { return _custOrder; } set { _custOrder = value; OnPropertyChanged(); } }

       public Customer(string name, string address, string phoneNo)
        {
            _name = name;
            _address = address;
            _phoneNo = phoneNo;
            _custOrder=new Order();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
