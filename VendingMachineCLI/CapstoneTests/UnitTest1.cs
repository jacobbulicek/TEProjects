using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class UnitTest1
        
    {
        private VendingMachine vendingMachine;
        private VendingMachineItem item;
        private VendingMachineItem item2;
        private VendingMachineItem item3;

        [TestInitialize]
        public void Setup()
        {
            
            item = new VendingMachineItem();
            item.ProductCode = "A1";
            item.Quantity = 3;
            item.Price = 1.00M;

            item2 = new VendingMachineItem();
            item2.Quantity = -1;
            item2.ProductCode = "A2";
            item2.Price = 0.00M;

            item3 = new VendingMachineItem();
            item3.Quantity = 5;
            item3.ProductCode = "A3";
            item3.Price = 100.00M;

            List<VendingMachineItem> vendingMachineItems = new List<VendingMachineItem>();
            vendingMachineItems.Add(item);
            vendingMachineItems.Add(item2);
            vendingMachineItems.Add(item3);

            vendingMachine = new VendingMachine(vendingMachineItems);
            


        }

        [TestMethod]
        public void FeedMoneyTest()
        {
            Assert.AreEqual(true, vendingMachine.FeedMoney(10));
            Assert.AreEqual(false, vendingMachine.FeedMoney(9));
        }

        [TestMethod]
        public void ChangeTest()
        {
            vendingMachine.FeedMoney(1);
            int[] test = new int[] { 4, 0, 0 };            

            CollectionAssert.AreEqual(test, vendingMachine.Change());
        }
        [TestMethod] 
        public void ValidIDTest()
        {           

            Assert.AreEqual(true, vendingMachine.ValidID("A1"));
            Assert.AreEqual(false, vendingMachine.ValidID("Hello"));

        }
        [TestMethod]
        public void InStockTest()
        {
            Assert.AreEqual(true, vendingMachine.InStock("A1"));
            Assert.AreEqual(false, vendingMachine.InStock("A2"));

        }
        [TestMethod]
        public void EnoughMoneyTest()
        {
            vendingMachine.FeedMoney(10);
            Assert.AreEqual(true, vendingMachine.EnoughMoney(item.ProductCode));
            Assert.AreEqual(false, vendingMachine.EnoughMoney(item3.ProductCode));
        }

        [TestMethod]
        public void UpdateVMTest()
        {
            vendingMachine.FeedMoney(10);
            vendingMachine.UpdateVM(item.ProductCode);
            Assert.AreEqual(9, vendingMachine.Balance);
            Assert.AreEqual(2, item.Quantity);

        }

    }
}
