using CustomerOrderViewer.Models;
using CustomerOrderViewer.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient; //Nuget csomag
using System.Linq;

namespace CustomerOrderViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CustomerOrderDetailCommand customerOrderDetailCommand = new CustomerOrderDetailCommand(@"Data Source=.\sqlexpress;Initial Catalog=CustomerOrderViewer;Integrated Security=True");

                IList<CustomerOrderDetailModel> customerOrderDetailModels = customerOrderDetailCommand.GetList();

                if (customerOrderDetailModels.Any())
                {
                    foreach (CustomerOrderDetailModel customerOrderDetailModel in customerOrderDetailModels)
                    {
                        Console.WriteLine($"{customerOrderDetailModel.CustomerOrderId}:" +
                            $" Fullname: {customerOrderDetailModel.FirstName} {customerOrderDetailModel.LastName }" +
                            $" (Id: {customerOrderDetailModel.CustomerId})" +
                            $"- purchased {customerOrderDetailModel.Description}" +
                            $" for {customerOrderDetailModel.Price}" +
                            $" (Id: {customerOrderDetailModel.ItemId})"); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }
    }
}
 