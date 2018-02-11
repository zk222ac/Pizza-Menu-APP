using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using App62.Annotations;
using App62.Model;
using System.Windows.Input;
using Windows.UI.Xaml;
using App62.ViewModel.Command;
using static App62.App;

namespace App62.ViewModel
{
    public class OrderVm : INotifyPropertyChanged
    {

        #region Fields
        private Pizza _selectedPizza;
        private OrderItem _ordItm;
        private Customer _newCustomer;
        private ObservableCollection<OrderItem> _orderItems;
        private Payments paymentsRecord = new Payments();
        private Visibility _customerStack = Visibility.Visible;
        private string _lackInCustomerInformation;
        private Visibility _selectedPizzaVisibilty;
        private Visibility _orderDetailsVisibility;
        private Visibility _paySpVisibility;

        #endregion

        #region Construction

        public OrderVm()
        {
            List<Pizza> temp = PizzaCatalog.GetPizzas();
            PizzaMenu = new ObservableCollection<Pizza>(temp);
            OderedItems = new ObservableCollection<OrderItem>();
            CreatOrderCommand = new RelayCommand(CreatOrder);
            AddToOrderCommand = new RelayCommand(AddToOrder);
            PayCommand = new RelayCommand(PayInvoice);
            _selectedPizzaVisibilty = Visibility.Collapsed;
            PaySpVisibility = Visibility.Collapsed;
            OrderDetailsVisibility = Visibility.Collapsed;
            
        }

        #endregion

        #region Properties
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Pizza SelectedPizza
        {
            get { return _selectedPizza; }
            set
            {
                if (_selectedPizza != value)
                {
                    _selectedPizza = value;
                    _ordItm = new OrderItem(_selectedPizza);
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(OrdItm));
                }
            }
        }
        public OrderItem OrdItm
        {
            get { return _ordItm; }
            set
            {
                if (_ordItm != value)
                {
                    _ordItm = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Pizza> PizzaMenu { get; set; }
        public Order Order { get; set; }
        public Customer NewCustomer
        {
            get { return _newCustomer; }
            set
            {
                _newCustomer = value;
                statCust = _newCustomer; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedPizzaVisibilty));
            }
        }
        public INotifyUser Notifier { get; set; }
        public ObservableCollection<OrderItem> OderedItems { get; set; }
        public Visibility CustomerStack { get { return _customerStack; } set { _customerStack = value; OnPropertyChanged(); } }
        public Visibility CustomerInformationInvoice
        {
            get
            {
                if (_customerStack == Visibility.Collapsed)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility SelectedPizzaVisibilty
        {
            get
            {
                return _selectedPizzaVisibilty;
            }
            set
            {
                if (NewCustomer!=null)
                {
                    _selectedPizzaVisibilty = value;
                    OnPropertyChanged();
                }
               
            }
        }

        public Visibility OrderDetailsVisibility { get { return _orderDetailsVisibility; } set { _orderDetailsVisibility = value; OnPropertyChanged(); } }
        public Visibility PaySpVisibility { get { return _paySpVisibility; } set { _paySpVisibility = value; OnPropertyChanged(); } }
        public string LackInCustomerInformation { get { return _lackInCustomerInformation; } set
        {
            _lackInCustomerInformation = value; OnPropertyChanged();
        } }

        public long CreditCardNo { get; set; }
        public int SecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        #endregion

        #region Command
        public RelayCommand CreatOrderCommand { get; set; }
        public RelayCommand AddToOrderCommand { get; set; }
        public RelayCommand PayCommand { get; set; }
        #endregion

        #region Methods
        public void CreatOrder()
        {

            if (String.IsNullOrWhiteSpace(Name) || String.IsNullOrWhiteSpace(Phone) || String.IsNullOrWhiteSpace(Address))
            {
                Notifier.Notify("Make sure you write all cutomer details"); 
                return;
            }
            NewCustomer = new Customer(Name, Address, Phone);
            CustomerStack = Visibility.Collapsed;
            SelectedPizzaVisibilty = Visibility.Visible;

        }

        private void AddToOrder()
        {

            bool f= NewCustomer.CustOrder.AddOrderItem(OrdItm);
            if (!f)
            {
                Notifier.Notify("you have this item in your order");
            }
            else
            {
                OrderDetailsVisibility= Visibility.Visible;
                PaySpVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(OrderDetailsVisibility));
                OnPropertyChanged(nameof(PaySpVisibility));
            }
        }

        private void PayInvoice()
        {

            if (!ValidateCreditcardNumber(CreditCardNo))
            {
                Notifier.Notify("The creditcard number is wrong");
                return;
            } else if (!ValidateSecurityCode(SecurityCode))
            {
                Notifier.Notify("Wrong Security Code");
                return;
            }
            else if (!ValidateDate(ExpirationDate))
            {
                Notifier.Notify("The card is already expaired");
                return;
            }
            else
            {
                Payment p = new Payment(CreditCardNo, NewCustomer.CustOrder.TotalPrice);
                paymentsRecord.payInvoice(p);
                NewCustomer = null;
                OnPropertyChanged(nameof(NewCustomer));
                CustomerStack = Visibility.Visible;
                OnPropertyChanged(nameof(CustomerStack));
                SelectedPizzaVisibilty=Visibility.Collapsed;
                OnPropertyChanged(nameof(SelectedPizzaVisibilty));
                OrderDetailsVisibility = Visibility.Collapsed;
                OnPropertyChanged(nameof(OrderDetailsVisibility));
                PaySpVisibility=Visibility.Collapsed;
                OnPropertyChanged(nameof(PaySpVisibility));
                
                Notifier.Notify("the payment is succeeded");




            }




        }

        private bool ValidateDate(DateTime expirationDate)
        {
            return true;
        }

        private bool ValidateSecurityCode(int securityCode)
        {
            return true;

        }

        private bool ValidateCreditcardNumber(long creditCardNo)
        {
            return true;

        }

        #endregion

        #region INotifyIntrefaceImplementation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
