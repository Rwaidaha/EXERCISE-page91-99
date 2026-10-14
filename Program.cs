using EXERCISE_page91_99.Models;
using System.Diagnostics;
using System.Transactions;
namespace EXERCISE_page91_99

{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ListOfUserTransactions = new List<TransactionModel>();

            // for point 1  from excersice
            Console.WriteLine("Choose the Operation: 1. Deposit  2. Withdraw  3. Check Balance");

            // for point 2 , 3 ,4  from excersice
            Console.WriteLine("4. Build a Dictionary<string, Account>  5. Run the list-versus-dictionary timing  6. every account number mapped to its own transaction list ");

            var Operation = int.Parse(Console.ReadLine() ?? "");

            if (Operation == 1)
            {
                Console.WriteLine("enter the nationalId");
                var UserNanationalId = Console.ReadLine() ?? "";
                var CustomerCredentials = CheckCustomerData(UserNanationalId);

                if (CustomerCredentials.Id == 0)
                {
                    Console.Write("No Such Customer exists");
                    return;
                }
                else
                {
                    Console.WriteLine("enter the deposit amount");
                    var DepositAmount = decimal.Parse(Console.ReadLine() ?? "");



                    var DepositTransaction = Depoist(CustomerCredentials, DepositAmount);
                    ListOfUserTransactions.Add(DepositTransaction);


                }

            }
            else if (Operation == 2)
            {
                Console.WriteLine("enter the nationalId");
                var UserNanationalId = Console.ReadLine() ?? "";
                var CustomerCredentials = CheckCustomerData(UserNanationalId);

                if (CustomerCredentials.Id == 0)
                {
                    Console.Write("No Such Customer exists");
                    return;
                }
                else
                {
                    Console.WriteLine("enter the withdraw amount");
                    var WithdeawAmount = decimal.Parse(Console.ReadLine() ?? "");



                    var WithdrawTransaction = Withdraw(CustomerCredentials, WithdeawAmount);
                    ListOfUserTransactions.Add(WithdrawTransaction);


                }

            }
            // the Request Of point 2 from Exercise


            else if (Operation == 4)
            {
                var Accounts = GetAccounts();

                Console.WriteLine("enter the account number");
                var AccountNumber = Console.ReadLine() ?? "";

                if (Accounts.TryGetValue(AccountNumber, out var FoundAccount))
                {
                    Console.WriteLine($"OwnerName: {FoundAccount.CustomerName}, Balance: {FoundAccount.Balance}");
                }
                else
                {
                    Console.WriteLine("There is no Account is Found");
                }
            }

            // the Request Of point 3 from Exercise

            else if (Operation == 5)
            {
                RunTiming();
            }





            // the Request Of point 4 from Exercise


            else if (Operation == 6)
            {
                var Accounts = GetAccounts();
                var TransactionsByAccount = new Dictionary<string, List<TransactionModel>>();

               
                foreach (var account in Accounts.Values)
                {
                    TransactionsByAccount.Add(account.AccountNumber, new List<TransactionModel>());
                }


                Console.WriteLine("enter ccount number");
                var AccountNumber = Console.ReadLine();

                if (Accounts.TryGetValue(AccountNumber, out var FoundAccount))
                {
                    
                    Console.WriteLine("enter deposit amount");
                    var DepositAmount = decimal.Parse(Console.ReadLine());

                    var NewTransaction = DepositToAccount(FoundAccount, DepositAmount);
                    TransactionsByAccount[AccountNumber].Add(NewTransaction);

                    
                    Console.WriteLine($"statement of account {AccountNumber}:");
                    foreach (var transaction in TransactionsByAccount[AccountNumber])
                    {
                        Console.WriteLine($"Date: {transaction.DateOfTransaction}, IsDeposit: {transaction.IsDepoist}, Balance: {transaction.Balance}, CustomerName: {transaction.CustomerName}");
                    }
                }
                
            }





            else
                {
                    Console.WriteLine("Invalid Operation");
                }


                foreach (var transaction in ListOfUserTransactions)
                {
                    Console.WriteLine($"CustomerId: {transaction.CustomerId}, IsDeposit: {transaction.IsDepoist}, Balance: {transaction.Balance}, CustomerName: {transaction.CustomerName}");
                }



            }
            static TransactionModel Depoist(Customer customer, decimal amount)
            {
                customer.Balance += amount;
                return new TransactionModel
                {
                    CustomerId = customer.Id,
                    IsDepoist = true,
                    DateOfTransaction = DateTime.Now,
                    CustomerName = customer.FullName,
                    Balance = customer.Balance
                };
            }

            static TransactionModel Withdraw(Customer customer, decimal amount)
            {
                customer.Balance -= amount;
                return new TransactionModel
                {
                    CustomerId = customer.Id,
                    IsDepoist = false,
                    DateOfTransaction = DateTime.Now,
                    CustomerName = customer.FullName,
                    Balance = customer.Balance
                };
            }



            static Customer CheckCustomerData(string NationalId)
            {
                List<Customer> Customers = new List<Customer>();
                var customer1 = new Customer(id: 1, fullName: "John Doe", nationalId: "123456789", phone: "555-1234", balance: 1000.00m, joinedAt: DateTime.Now);
                var customer2 = new Customer(id: 2, fullName: "Jane Smith", nationalId: "987654321", phone: "555-5678", balance: 500.00m, joinedAt: DateTime.Now);


                Customers.Add(customer1);
                Customers.Add(customer2);


                var CheckCustomer = Customers.FirstOrDefault(c => c.NationalId == NationalId);
                if (CheckCustomer is null)
                {
                    return new Customer(id: 0, fullName: "", nationalId: "", phone: "", balance: 0.0m, joinedAt: DateTime.Now);
                }
                else
                {
                    Console.WriteLine(CheckCustomer.Balance);
                    return CheckCustomer;

                }
            }



            // function for point 2 (Dictionary)
            static Dictionary<string, Account> GetAccounts()
            {
                var Accounts = new Dictionary<string, Account>();

                var Account1 = new Account(accountNumber: "2000030661", customerName: "John Doe", balance: 1000.00m);
                var Account2 = new Account(accountNumber: "2000030662", customerName: "Jane Smith", balance: 500.00m);
                var Account3 = new Account(accountNumber: "2000030663", customerName: "rwaidaha", balance: 10000.00m);
                var Account4 = new Account(accountNumber: "2000030665", customerName: "ahmed", balance: 5000.00m);
                var Account5 = new Account(accountNumber: "2000030664", customerName: "zain", balance: 2000.00m);
                var Account6 = new Account(accountNumber: "2000030668", customerName: "karar", balance: 3000.00m);

                Accounts.Add(Account1.AccountNumber, Account1);
                Accounts.Add(Account2.AccountNumber, Account2);
                Accounts.Add(Account3.AccountNumber, Account3);
                Accounts.Add(Account4.AccountNumber, Account4);
                Accounts.Add(Account5.AccountNumber, Account5);
                Accounts.Add(Account6.AccountNumber, Account6);

                return Accounts;
            }

            static void RunTiming()
            {
                var ListOfAccounts = new List<Account>();
                var DictionaryOfAccounts = new Dictionary<string, Account>();


                // i use  loop for 6 account number rather than 500 000 

                for (int i = 1; i <= 6; i++)
                {
                    var NewAccount = new Account(accountNumber: "20000-" + i, customerName: "Customer" + i, balance: 100.00m);
                    ListOfAccounts.Add(NewAccount);
                    DictionaryOfAccounts.Add(NewAccount.AccountNumber, NewAccount);
                }

                var LastAccountNumber = "2000030668";
                var FirstAccountNumber = "2000030661";


                Console.WriteLine($"List:       {SearchInList(ListOfAccounts, LastAccountNumber)}");
                Console.WriteLine($"Dictionary: {SearchInDictionary(DictionaryOfAccounts, LastAccountNumber)}");


                Console.WriteLine($"List:       {SearchInList(ListOfAccounts, FirstAccountNumber)}");
                Console.WriteLine($"Dictionary: {SearchInDictionary(DictionaryOfAccounts, FirstAccountNumber)}");





                static double SearchInList(List<Account> accounts, string accountNumber)
                {
                    var Timer = Stopwatch.StartNew();

                    Timer.Stop();
                
                }


                static double SearchInDictionary(Dictionary<string, Account> accounts, string accountNumber)
                {
                    var Timer = Stopwatch.StartNew();

                    for (int r = 0; r < 6; r++)
                    {

                        accounts.ContainsKey(accountNumber);
                    }

                    Timer.Stop();
                
                }


            }


            // function  followw for point 4 from Exercise
            static TransactionModel DepositToAccount(Account account, decimal amount)
            {
                account.Balance += amount;
                return new TransactionModel
                {
                    IsDepoist = true,
                    DateOfTransaction = DateTime.Now,
                    CustomerName = account.CustomerName,
                    Balance = account.Balance
                };
            }


        }
    }
}
