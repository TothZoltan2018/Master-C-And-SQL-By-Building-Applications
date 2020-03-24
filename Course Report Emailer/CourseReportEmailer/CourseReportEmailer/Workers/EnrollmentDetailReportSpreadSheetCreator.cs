using CourseReportEmailer.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CourseReportEmailer.Workers
{    
    class EnrollmentDetailReportSpreadSheetCreator
    {
        public void Create(string fileName, IList<EnrollmentDetailReportModel> enrollmentModels)
        {
            //Nuget csomagot installatunk: DocumentFormat.OpenXml
            //Az alabbi dokumentumszerkezet felepitese:
            //SpreadsheetDocument -- WorkbookPart 
            //                          |       |
            //                      WorkBook    |
            //                          |       |-- WorksheetPart -- Worksheet(SheetData)
            //                       Sheets             |
            //                          |               |
            //                        Sheet.Id <--------|
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                //C# object (enrollmentModels) --> Json --> DataTable
                var json = JsonConvert.SerializeObject(enrollmentModels);
                DataTable enrollmentsTable = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable)); //Ezt lehet majd az Excelnek beadni

                // WorkbookPart is an Object that contains global settings about all of the components in a Workbook.
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();                
                SheetData sheetData = new SheetData(); //SheetData: ebbe tesszuk majd a DataTable-unket.
                worksheetPart.Worksheet = new Worksheet(sheetData);

                //Most kapcsoljuk ossze a fent letrehozott Workbook-ot a Worksheet-tel. 
                //Hozzaadunk egy lista ures Sheet-et.
                Sheets sheetList = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                //Hozzunk letre egy generikus Sheet-et.
                Sheet singleSheet = new Sheet()
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart), //WorkBook - WorkSheet osszekapcsolas ==> Sheet.Id
                    SheetId = 1,
                    Name ="Report Sheet"
                };
                //Adjunk hozza a most letrehozott Sheet-et.
                sheetList.Append(singleSheet);
            }

        }
    }
}
