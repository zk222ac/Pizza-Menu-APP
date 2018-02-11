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
using App62.Model;
using App62.ViewModel;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App62
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderPage : Page, INotifyUser
    {
        public OrderPage()
        {
            this.InitializeComponent();
            ((OrderVm)DataContext).Notifier = this;
            //SelectedPizzaPanel.Visibility = Visibility.Collapsed;


        }


        //private void GridList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
           
        //    SelectedPizzaPanel.Visibility=Visibility.Visible;
        //}

        public void Notify(object content)
        {
            MessageDialog dlg = new MessageDialog(content.ToString());
            dlg.ShowAsync();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }
    }
}
