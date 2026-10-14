using System;
using System.Collections.Generic;
using System.Text;

namespace EXERCISE_page91_99.Models
{
    internal class Customer
    {
        public Customer(int id, string fullName, string nationalId, string phone, decimal balance, DateTime joinedAt)
        {
            Id = id;
            FullName = fullName;
            NationalId = nationalId;
            Phone = phone;
            Balance = balance;
            JoinedAt = joinedAt;
        }



        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string NationalId { get; set; } = "";
        public string Phone { get; set; } = "";
        public decimal Balance { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
