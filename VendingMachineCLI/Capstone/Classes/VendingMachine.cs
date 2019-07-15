using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private List<VendingMachineItem> vendingMachineItems = new List<VendingMachineItem>();
        private string filePath = @"C:\VendingMachine\vendingmachine.csv";
        

        public VendingMachine()
        {

        }

        public VendingMachine(List<VendingMachineItem> vendingMachineItems)
        {
            this.vendingMachineItems = vendingMachineItems;
        }

        public decimal Balance { get; private set; }
        

        Logger log = new Logger();


        public void ReadItems()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        VendingMachineItem item = new VendingMachineItem();
                        string line = sr.ReadLine();

                        string[] fields = line.Split('|');

                        item.ProductCode = fields[0];
                        item.Name = fields[1];
                        item.Price = decimal.Parse(fields[2]);

                        vendingMachineItems.Add(item);
                    }
                }
            }
            catch
            {
                vendingMachineItems = new List<VendingMachineItem>();
            }
            return;
        }

        public List<VendingMachineItem> GetItems()
        {
            return vendingMachineItems;
        }

        public bool FeedMoney(int amount)
        {
            bool result = false;
           
                if (amount == 1 || amount == 2 || amount == 5 || amount == 10)
                {
                    Balance += amount;
                    result = true;
                }
                log.Log("FEED MONEY", Balance - amount, Balance);                
            
            return result;
        }

        public int[] Change()
        {
            decimal initialBalance = Balance;
            int[] changearray = new int[3];

            changearray[0] = (int)(Balance * 4);
            Balance -= (decimal)changearray[0] / 4;
            changearray[1] = (int)(Balance * 10);
            Balance -= (decimal)changearray[1] / 10;
            changearray[2] = (int)(Balance * 20);
            Balance = 0;

            log.Log("GIVE CHANGE", initialBalance, Balance);

            return changearray;
        }

        public bool ValidID(string itemCode)
        {
            bool result = false;
            for (int i = 0; i < vendingMachineItems.Count; i++)
            {
                if (vendingMachineItems[i].ProductCode == itemCode)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool InStock(string itemCode)
        {
            bool inStock = false;
            for (int i = 0; i < vendingMachineItems.Count; i++)
            {
                if (vendingMachineItems[i].Quantity > 0 && vendingMachineItems[i].ProductCode == itemCode)
                {
                    inStock = true;
                }
            }
            return inStock;
        }

        public bool EnoughMoney(string itemCode)
        {
            bool enoughMoney = false;
            for (int i = 0; i < vendingMachineItems.Count; i++)
            {
                if (vendingMachineItems[i].Price <= Balance && vendingMachineItems[i].ProductCode == itemCode)
                {
                    enoughMoney = true;
                }
            }
            return enoughMoney;
        }

        public void UpdateVM(string itemCode)
        {
            for (int i = 0; i < vendingMachineItems.Count; i++)
            {
                if (vendingMachineItems[i].ProductCode == itemCode)
                {
                    decimal initialBalance = Balance;
                    vendingMachineItems[i].Quantity--;
                    Balance -= vendingMachineItems[i].Price;

                    log.Log($"{vendingMachineItems[i].Name} {vendingMachineItems[i].ProductCode}", initialBalance, Balance);
                }                

            }
        }

        public string ItemMessage(string itemCode)
        {
            string response = "";

            if (itemCode.StartsWith('A'))
            {
                response = "Crunch Crunch, Yum!";
            }
            else if (itemCode.StartsWith('B'))
            {
                response = "Munch Munch, Yum!";
            }
            else if (itemCode.StartsWith('C'))
            {
                response = "Glug Glug, Yum!";
            }
            else if (itemCode.StartsWith('D'))
            {
                response = "Chew Chew, Yum!";
            }
            return response;
        }
    }
}


