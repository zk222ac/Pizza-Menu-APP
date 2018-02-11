using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using App62.Model;
using App62.ViewModel;
using App62.ViewModel.Command;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace PizzaShopTest
{
    [TestClass]
    public class UnitTest1
    {
        PizzaMenuVm testPizzaMenuVm = new PizzaMenuVm();
        OrderVm testOrderVm = new OrderVm();
        [TestMethod]
        public void TestMenuList()
        {
            Assert.AreEqual(testPizzaMenuVm.PizzaMenu.Count, 12);
        }
        [TestMethod]
        public void TesTAddPizza()
        {
            Assert.IsNotNull(testPizzaMenuVm.PizzaMenu);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("Cheese","Large","50"),50);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("Cheese", "Large", "a50"), -2);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("Cheese", "", "50"), -1);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("", "Large", "50"), -1);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("Cheese", "Large", ""), -2);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("Cheese", "Large", null), -2);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry(null, "Large", "50"), -1);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("Cheese", null, "50"), -1);
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("Cheese", "Large", "-20"), -1); // negative price
            Assert.AreEqual(testPizzaMenuVm.ValidateEntry("Chilly", "Extra Large", "70"),0); // test to prevent dublicate
        }
        [TestMethod]
        public void TestCreatAndAddOrder()
        {
           testOrderVm.Name = "Sameer";
            testOrderVm.Address = "Tekroner 25, 2 tv";
            testOrderVm.Phone = "22 44 5784";
            testOrderVm.CreatOrder();
            Assert.IsNotNull(testOrderVm.NewCustomer);
            Assert.IsNotNull(testOrderVm.NewCustomer.CustOrder);
            Assert.AreEqual(testOrderVm.NewCustomer.CustOrder.OrderItemsList.Count,0);
            testOrderVm.SelectedPizza = testOrderVm.PizzaMenu[1];
            testOrderVm.NewCustomer.CustOrder.AddOrderItem(testOrderVm.SelectedPizza, 5);
            Assert.IsNotNull(testOrderVm.NewCustomer.CustOrder.OrderItemsList[0]);
            testOrderVm.SelectedPizza = testOrderVm.PizzaMenu[3];
            testOrderVm.NewCustomer.CustOrder.AddOrderItem(testOrderVm.SelectedPizza, 2);
            Assert.AreEqual(testOrderVm.NewCustomer.CustOrder.OrderItemsList.Count,2);
            Assert.AreEqual(testOrderVm.NewCustomer.CustOrder.TotalPrice, 400 );

        }
    }
}

