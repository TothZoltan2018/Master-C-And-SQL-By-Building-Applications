using CustomerOrderViewer2._0.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomerOrderViewer2._0.Repository
{
    class CustomerCommand
    {
        private string _connectionString;

        public CustomerCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<CustomerModel> GetList()
        {
            List<CustomerModel> items = new List<CustomerModel>();

            //Installaltuk a Dapper nuget csomagot az SQL serverrel valo kapcsolat egyszerubbe tetelehez. Automatikusan megfelelteti a DB tabla mezoit a Csharp modell property-jeihez
            var sqlSelectStatement = "Customer_GetList"; //Ez a korabban az SQL SMS-ban megirt egyik stored procedurank neve

            using (SqlConnection connection = new SqlConnection(_connectionString)) //Beirtuk, hogy "SqlConnection", es ctrl+. --> install package, utana berakta: using System.Data.SqlClient;
            {
                items = connection.Query<CustomerModel>(sqlSelectStatement).ToList(); //Beirtuk, hogy "connection.Query", es ctrl+. --> berakta: using Dapper;
            }

            return items;

        }
    }
}