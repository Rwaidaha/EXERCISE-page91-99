using System;
using System.Collections.Generic;
using System.Text;

namespace EXERCISE_page91_99.Models
{
    internal class Account
    {
        public Account(string accountNumber, string customerName, decimal balance)
        {
            AccountNumber = accountNumber;
            CustomerName = customerName;
            Balance = balance;
        }

        public string AccountNumber { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public decimal Balance { get; set; }
    }
}
