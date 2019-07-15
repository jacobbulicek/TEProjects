using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();


        public void RunInterface()
        {
            Prompt();
            bool done = false;
            vendingMachine.ReadItems();

            while (!done)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DisplayItems();
                        break;
                    case "2":
                        bool donePurchasing = false;
                        while (!donePurchasing)
                        {
                            PurchasePrompt();
                            string selection = Console.ReadLine();
                            if (selection == "1")
                            {
                                Console.WriteLine("Please enter the bill amount to feed: ");
                                int feedMoney = 0;
                                try
                                {
                                    feedMoney = int.Parse(Console.ReadLine());
                                    if (vendingMachine.FeedMoney(feedMoney))
                                    {
                                        Console.WriteLine("Thank you.");
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sorry, this machine can only accept US$ bills up to 10. Press enter to return to the purchase menu.");
                                        Console.ReadLine();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error has occurred.");
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine("Please enter a valid amount.");
                                }
                            }
                            if (selection == "2")
                            {
                                Console.WriteLine();
                                DisplayItems();

                                Console.WriteLine("Please enter a code for the item above that you wish to purchase.");
                                string answer = Console.ReadLine();


                                if (!vendingMachine.ValidID(answer))
                                {
                                    Console.WriteLine("Sorry this is not a valid code.");

                                }
                                else if (!vendingMachine.InStock(answer))  // if item is not in stock
                                {
                                    Console.WriteLine("Sorry the item is out of stock.");
                                }
                                else if (!vendingMachine.EnoughMoney(answer)) // if current balance is sufficient
                                {
                                    Console.WriteLine("Sorry you have insufficient funds.");
                                }
                                else
                                {
                                    vendingMachine.UpdateVM(answer);
                                    Console.WriteLine(vendingMachine.ItemMessage(answer));
                                    Console.WriteLine();
                                }

                            }

                            if (selection == "3")
                            {
                                int[] change = vendingMachine.Change();
                                Console.WriteLine("Here is your change: " + change[0] + " Quarters & " + change[1] + " Dimes & " + change[2] + " Nickels.");

                                Console.WriteLine();
                                Console.WriteLine("Press enter to return to the main menu.");
                                Console.ReadLine();

                                donePurchasing = true;
                            }
                        }

                        break;

                    case "3":
                        done = true;
                        break;
                }

                Prompt();
            }
        }

        void DisplayItems()
        {
            List<VendingMachineItem> items = vendingMachine.GetItems();
            Console.WriteLine("Code  ItemName                 Price    Qty");
            Console.WriteLine("-------------------------------------------");
            foreach (VendingMachineItem item in items)
            {
                Console.WriteLine($"{item.ProductCode}    {item.Name.PadRight(20)}     ${item.Price.ToString("0.00")}     {item.Quantity}");
            }
            Console.WriteLine();
        }

        public void Prompt()
        {
            Console.WriteLine("Please make a selection.");
            Console.WriteLine("1 - Display vending machine items.");
            Console.WriteLine("2 - Purchase.");
            Console.WriteLine("3 - End.");
        }

        public void PurchasePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Purchase Menu");
            Console.WriteLine("1 - Feed money.");
            Console.WriteLine("2 - Select product.");
            Console.WriteLine("3 - Finish transaction.");
            Console.WriteLine("Current Money Provided: " + "$" + vendingMachine.Balance.ToString("0.00"));
        }
    }
}

