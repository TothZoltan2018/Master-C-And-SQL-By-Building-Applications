using CustomerOrderViewer2._0.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomerOrderViewer2._0.Repository
{
    class ItemCommand
    {
        private string _connectionString;

        public ItemCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<ItemModel> GetList()
        {
            List<ItemModel> items = new List<ItemModel>();

            //Installaltuk a Dapper nuget csomagot az SQL serverrel valo kapcsolat egyszerubbe tetelehez. Automatikusan megfelelteti a DB tabla mezoit a Csharp modell property-jeihez
            var sqlSelectStatement = "Item_GetList"; //Ez a korabban az SQL SMS-ban megirt egyik stored procedurank neve

            using (SqlConnection connection = new SqlConnection(_connectionString)) //Beirtuk, hogy "SqlConnection", es ctrl+. --> install package, utana berakta: using System.Data.SqlClient;
            {
                items = connection.Query<ItemModel>(sqlSelectStatement).ToList(); //Beirtuk, hogy "connection.Query", es ctrl+. --> berakta: using Dapper;
            }
            
            return items;

        }
    }
}
