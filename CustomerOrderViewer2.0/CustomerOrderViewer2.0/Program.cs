using CustomerOrderViewer2._0.Models;
using CustomerOrderViewer2._0.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerOrderViewer2._0
{
    class Program
    {
        private static string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=CustomerOrderViewer;Integrated Security=True";
        private static CustomerOrderCommand _customerOrderCommand = new CustomerOrderCommand(_connectionString);
        private static CustomerCommand _customerCommand = new CustomerCommand(_connectionString);
        private static ItemCommand _itemCommand = new ItemCommand(_connectionString);

        static void Main(string[] args)
        {
            try
            {
                var continueManaging = true;
                var userId = string.Empty;

                Console.WriteLine("Please enter your username.");
                userId = Console.ReadLine();

                do
                {
                    Console.WriteLine("1- Show All | 2 - Upsert Customer Order | 3 - Delete Customer Order | 4 - Exit");

                    int option = Convert.ToInt32(Console.ReadLine());
                    //option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            ShowAll();
                            break;
                        case 2:
                            UpsertCustomerOrder(userId);
                            break;
                        case 3:
                            DeleteCustomerOrder(userId);
                            break;
                        case 4:
                            continueManaging = false;
                            break;
                        default:
                            Console.WriteLine("Option not found.");
                            break;
                    }

                } while (continueManaging);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }

        private static void ShowAll()
        {
            Console.WriteLine($"{Environment.NewLine}All Customer Orders:{Environment.NewLine}");
            DisplayCustomerOrders();

            Console.WriteLine($"{Environment.NewLine}All Customer:{Environment.NewLine}");
            DisplayCustomers();

            Console.WriteLine($"{Environment.NewLine}All Items:{Environment.NewLine}");
            DisplayItems();

            Console.WriteLine();
        }

        private static void DisplayItems()
        {
            IList<ItemModel> items = _itemCommand.GetList();
            if (items.Any())
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.ItemId}: Description: {item.Description}, Price: {item.Price}");
                }
            }
        }

        private static void DisplayCustomers()
        {
            IList<CustomerModel> custormers = _customerCommand.GetList();
            if (custormers.Any())
            {
                foreach (var customer in custormers)
                {
                    Console.WriteLine($"" +
                        $"{customer.CutomerId}: " +
                        $"First Name: {customer.FirstName}, " +
                        $"Middle Name: {customer.MiddleName ?? "N/A"}, " +
                        $"Last Name: { customer.LastName}, " +
                        $"Age: { customer.Age}");
                }
            }
        }

        private static void DisplayCustomerOrders()
        {
            IList<CustomerOrderDetailModel> custormerOrderDetails = _customerOrderCommand.GetList();
            if (custormerOrderDetails.Any())
            {
                foreach (var custormerOrderDetail in custormerOrderDetails)
                {
                    Console.WriteLine($"" +
                        $"{custormerOrderDetail.CustomerOrderId}: " +
                        $"Full Name: {custormerOrderDetail.FirstName}, {custormerOrderDetail.LastName}" +                        
                        $"(Id: {custormerOrderDetail.CustomerId}) - purchased " +
                        $"{custormerOrderDetail.Description} for " +
                        $"{custormerOrderDetail.Price} " +
                        $"({custormerOrderDetail.ItemId})"  );
                }                
            }
        }

        private static void DeleteCustomerOrder(string userId)
        {
            Console.WriteLine("Enter CustomerOrderId to be deleted:");
            int customerOrderId = Convert.ToInt32(Console.ReadLine());

            _customerOrderCommand.Delete(customerOrderId, userId);            
        }

        private static void UpsertCustomerOrder(string userId)
        {
            Console.WriteLine("Note: For updating insert existing CustomerOrderId, for new entries enter -1.");

            Console.WriteLine("Enter CustomerOrderId:");
            int newCustomerOrderId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter CustomerId:");
            int newCustomeId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter ItemId:");
            int newItemId = Convert.ToInt32(Console.ReadLine());

            _customerOrderCommand.Upsert(newCustomerOrderId, newCustomeId, newItemId, userId);
        }
    }
}
