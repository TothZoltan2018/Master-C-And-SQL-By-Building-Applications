using CustomerOrderViewer2._0.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomerOrderViewer2._0.Repository
{
    class CustomerOrderCommand
    {
        private string _connectionString;

        public CustomerOrderCommand(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void Upsert(int customerOrderId, int customerId, int itemId, string userId)
        {
            var upserStatement = "CustomerOrderDetail_Upsert";

            var dataTable = new DataTable(); //Az SQl SMS-ban csinaltunk egy User definied data type-ot. Ez annak a megfeleloje.
            dataTable.Columns.Add("CustomerOrderId", typeof(int));
            dataTable.Columns.Add("CustomerId", typeof(int));
            dataTable.Columns.Add("ItemId", typeof(int));
            dataTable.Rows.Add(customerOrderId, customerId, itemId);

            using (SqlConnection connection = new SqlConnection(_connectionString)) //Beirtuk, hogy "SqlConnection", es ctrl+. --> install package, utana berakta: using System.Data.SqlClient;
            {
                connection.Execute(upserStatement, new { @CustomerOrderType = dataTable.AsTableValuedParameter("CustomerOrderType"), @UserId = userId }, commandType: CommandType.StoredProcedure);
            }
        }
        public void Delete(int customerOrderId, string userId)
        {
            var upserStatement = "CustomerOrderDetail_Delete";

            using (SqlConnection connection = new SqlConnection(_connectionString)) //Beirtuk, hogy "SqlConnection", es ctrl+. --> install package, utana berakta: using System.Data.SqlClient;
            {
                connection.Execute(upserStatement, new { @CustomerOrderId = customerOrderId, @UserId = userId }, commandType: CommandType.StoredProcedure);
            }
        }

        public IList<CustomerOrderDetailModel> GetList()
        {
            List<CustomerOrderDetailModel> items = new List<CustomerOrderDetailModel>();

            //Installaltuk a Dapper nuget csomagot az SQL serverrel valo kapcsolat egyszerubbe tetelehez. Automatikusan megfelelteti a DB tabla mezoit a Csharp modell property-jeihez
            var sqlSelectStatement = "CustomerOrderDetail_GetList"; //Ez a korabban az SQL SMS-ban megirt egyik stored procedurank neve

            using (SqlConnection connection = new SqlConnection(_connectionString)) //Beirtuk. hogy "SqlConnection", es ctrl+. --> install package, utana berakta: using System.Data.SqlClient;
            {
                items = connection.Query<CustomerOrderDetailModel>(sqlSelectStatement).ToList(); //Beirtuk. hogy "connection.Query", es ctrl+. --> berakta: using Dapper;
            }

            return items;

        }
    }
}
