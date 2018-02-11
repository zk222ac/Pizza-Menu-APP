using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App62
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(HomePage));
            BackButton.Visibility = Visibility.Collapsed;
            AdminItem.Visibility=Visibility.Collapsed;
            HomeItem.IsSelected = true;

        }

        

        private void HamburgerButton_OnClick(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeItem.IsSelected)
            {
                BackButton.Visibility = Visibility.Collapsed;
                MyFrame.Navigate(typeof(OrderPage));
                PageName.Text = "Order";
            } else if (FoodItem.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(HomePage));
                PageName.Text = "Home";
            } else if (AdminItem.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(AdminPage));
                PageName.Text = "Admin";
            }
           
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                
                MyFrame.GoBack();
                BackButton.Visibility = Visibility.Visible;
                HomeItem.IsSelected = true;
            }
            
        }

        private void SignButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (UserNameBox.Text == "Admin")
            {
                if (PasswordBox.Password == "Admin")
                {
                    App.LoginStatus = true;
                    AdminItem.Visibility = Visibility.Visible;
                    MainSignButton.Visibility = Visibility.Collapsed;
                    MainSignOut.Visibility = Visibility.Visible;
                    MyFlyOut.Hide();
                }
                else
                {
                    ErorrMessage.Text = "Wrong Password";
                    PasswordBox.Password = "";
                }

            }
            else
            {
                ErorrMessage.Text = "Wrong User name or Password";
                PasswordBox.Password = "";
                UserNameBox.Text = "";
            }
            PasswordBox.Password = "";
            UserNameBox.Text = "";
        }

        private void MainSignOut_OnClick(object sender, RoutedEventArgs e)
        {
            App.LoginStatus = false;
            MainSignOut.Visibility = Visibility.Collapsed;
            MainSignButton.Visibility= Visibility.Visible;
            AdminItem.Visibility=Visibility.Collapsed;
            MyFrame.Navigate(typeof(OrderPage));
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
           MyFlyOut.Hide();
        }
    }
}
