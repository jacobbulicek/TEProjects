namespace Capstone
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class Logger
    {
        private string filePath = @"C:\VendingMachine\Log.txt";

        public void Log(string message, decimal moneyBeforeTransaction, decimal moneyAfterTransaction)
        {
            DateTime date = DateTime.Now;

            string dateString = date.ToString("MM/dd/yyyy hh:mm:ss tt");

            string moneyBeforeString = moneyBeforeTransaction.ToString("0.00");

            string moneyAfterString = moneyAfterTransaction.ToString("0.00");

            string logLine = $"{dateString} {message} ${moneyBeforeString} ${moneyAfterString}";            

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine(logLine);
                }
            }
            catch
            {
                Console.WriteLine("Unable to log, please check!");
                return;
            }
        }
    }
}
