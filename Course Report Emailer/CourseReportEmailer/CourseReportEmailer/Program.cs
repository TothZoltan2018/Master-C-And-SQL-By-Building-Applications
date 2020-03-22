using CourseReportEmailer.Models;
using Newtonsoft.Json;
using System;

namespace CourseReportEmailer
{
    class Program
    {
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
        }
    }
}
