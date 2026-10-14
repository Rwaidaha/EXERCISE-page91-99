using System;
using System.Collections.Generic;
using System.Text;

namespace EXERCISE_page91_99.Models
{
    internal class TransactionModel
    {
        public int CustomerId { get; set; }
        public bool IsDepoist { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public string CustomerName { get; set; } = "";
        public decimal Balance { get; set; }
    }
}
