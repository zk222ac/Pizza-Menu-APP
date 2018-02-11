using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using App62.Model;
using System.Windows.Input;
using App62.ViewModel.Command;

namespace App62.ViewModel
{
    public class PizzaMenuVm : INotifyPropertyChanged
    {
        #region Fields

        private string _type;
        private string _size;
        private string _price;
        private string _errorMessage;

        private ObservableCollection<Pizza> _pizzaMenu;

        #endregion

        #region Construction
        public PizzaMenuVm()
        {
            _pizzaMenu = new ObservableCollection<Pizza>(PizzaCatalog.GetPizzas());
            AddCommand = new RelayCommand(AddPizza);
            LoadCommand = new RelayCommand(LoadPizzaMenu);
            SaveCommand = new RelayCommand(SavePizzaMenu);
        }

        #endregion

        #region Properties

        public string Type { get {return _type;} set { if (_type!=value) { _type = value; OnPropertyChanged(); 
                }
            }}
        public string Size { get { return _size; } set { if (_size != value) { _size = value; OnPropertyChanged(); } } }
        public string Price { get { return _price; } set { if (_price != value) { _price = value; OnPropertyChanged(); } } }
        public string ErrorMessage { get { return _errorMessage; } set { if (_errorMessage != value) { _errorMessage = value; OnPropertyChanged(); } } }
        public ObservableCollection<Pizza> PizzaMenu {
            get { return _pizzaMenu; }
            set {
                 if (_pizzaMenu!=value)
                 {
                _pizzaMenu = value;
                    OnPropertyChanged("PizzaMenu");
                 }
            }
        }
        public INotifyUser Notifier { get; set; }
        #endregion

        #region Command
        public RelayCommand AddCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand LoadCommand { get; set; }
        #endregion

        #region Method
        public void AddPizza()
        {
            decimal price = ValidateEntry(Type, Size, Price);
            switch ((int)price)
            {
                case -2:
                    Notifier.Notify("Please enter Valid price");
                    //ErrorMessage = "Please enter Valid price";
                   // OnPropertyChanged("ErrorMessage");
                    break;
                case -1:
                    Notifier.Notify("Please enter type and size of pizza");
                  //  ErrorMessage = "Please enter type and size of pizza";
                  //  OnPropertyChanged("ErrorMessage");
                    break;
                case 0:
                    Notifier.Notify("The Pizza is already exicted in menu list");
                  //  ErrorMessage = "The Pizza is already exicted in menu list";
                 //   OnPropertyChanged("ErrorMessage");
                    break;
                default:
                    _pizzaMenu.Add(new Pizza(Type, Size, price));
                    break;
            }
          
            RestProperty();
        }

        public decimal ValidateEntry(string type, string size, string price)
        {

            decimal tempPrice = 0;
            bool isDecimal = Decimal.TryParse(price, out tempPrice);
            if (!isDecimal)
            {
                return -2;
            }

            if (String.IsNullOrEmpty(type) || String.IsNullOrEmpty(size) || !isDecimal || tempPrice <= 0)
            {
                return -1;
            }
            string typeSize = "Pizza Type: " + type + ": Size: " + size;
            if (PizzaMenu.Count != 0)
            {
                foreach (var item in PizzaMenu)
                {
                    string s = item.ToString();
                    if (s.Contains(typeSize))
                    {
                        return 0;
                    }
                }
            }
            return tempPrice;
        }


        public void RestProperty()
        {
            Type = "";
            Size = "";
            Price = "";
            OnPropertyChanged("Type");
            OnPropertyChanged("Size");
            OnPropertyChanged("Price");
        }

        public async void SavePizzaMenu()
        {
            Facade.SavePizzasAsXmlAsync(PizzaMenu);
        }

        public async void LoadPizzaMenu()
        {
            ObservableCollection<Pizza> pizzas = await Facade.LoadPizzasFromXmlAsync();
            PizzaMenu.Clear();
            foreach (var pizza in pizzas)
            {
                PizzaMenu.Add(pizza);
            }

        }

        #endregion

        #region INotifyIntrefaceImplementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
