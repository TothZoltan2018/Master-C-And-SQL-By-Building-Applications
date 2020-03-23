using CourseReportEmailer.Models;
using Newtonsoft.Json;
using System;
using System.Data;

namespace CourseReportEmailer
{
    class Program
    {
        public static DataColumn DataColumn { get; private set; }

        static void Main(string[] args)
        {
            EnrollmentDetailReportModel model = new EnrollmentDetailReportModel()
            {
                EnrollmentId = 1,
                FirstName = "Mark",
                LastName = "Hue",
                CourseCode = "CA",
                Description = "Some description",
            };

            //Json nuget package-et installaltuk
            //Json frmatumba tesszuk a C# objektumot
            var json = JsonConvert.SerializeObject(model);
            //Visszalalakitjuk az elobbit
            EnrollmentDetailReportModel objectFromJson = (EnrollmentDetailReportModel)JsonConvert.DeserializeObject(json, typeof(EnrollmentDetailReportModel));

            DataTable table = new DataTable();

            DataColumn column1 = new DataColumn("EnrollmentId", typeof(int));
            DataColumn column2 = new DataColumn("FirstName", typeof(string));
            DataColumn column3 = new DataColumn("LastName", typeof(string));
            DataColumn column4 = new DataColumn("CourseCode", typeof(string));
            DataColumn column5 = new DataColumn("Description", typeof(string));

            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);

            table.Rows.Add(1, "Mark", "Hue", "CA", "Some description");
            //table.Rows.Add(true, "Mark", 7, "CA", "Some description"); //Inkonzisztens adattipusok eseten atkonvertalja az oszlop tipusara - gondolom, ha lehet

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine(row[column]);
                }
            }

            //C# object --> Json --> DataTable
        }
    }
}
