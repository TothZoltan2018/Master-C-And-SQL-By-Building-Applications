using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CustomerOrderViewer.Models;

namespace CustomerOrderViewer.Repository
{
    //SqlConnection
    //SqlCommand
    //SqlDataReader
    //SqlCommand.ExecuteReader(): Sends the SqlCommand.CommandText to the SqlCommand.Connection and builds a System.Data.SqlClient.SqlDataReader.
    //SqlDataReader.Read():Reads a line and increments its pointer.
    class CustomerOrderDetailCommand
    {
        private string _connectionString;

        public CustomerOrderDetailCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        //List of read database View lines. IList: best practice to return interface instead of a concrete list.
        public IList<CustomerOrderDetailModel> GetList()
        {
            List<CustomerOrderDetailModel> customerOrderDetailModels = new List<CustomerOrderDetailModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT CustomerOrderId, CustomerId, ItemId, FirstName, LastName, [Description], Price FROM CustomerOrderDetail", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())//executes the sql command in " " through connection and gives back a reader
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CustomerOrderDetailModel customerOrderDetailModel = new CustomerOrderDetailModel()
                                {
                                    //CustomerOrderId = (int)reader["CustomerOrderId"]; //Ez nem jo?
                                    CustomerOrderId = Convert.ToInt32(reader["CustomerOrderId"]),
                                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                    ItemId = Convert.ToInt32(reader["ItemId"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"])
                                };

                                customerOrderDetailModels.Add(customerOrderDetailModel);
                            }                            
                        }
                    }
                }
 
            }
            
            return customerOrderDetailModels;
        }
    }
}
