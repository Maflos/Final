using System;
using System.IO;
using System.Text;

namespace LibraryContracts.Reports
{
    class DocumentPDF : DocumentGeneral
    {
        public override void GenerateReport()
        {
            var csv = new StringBuilder();

            foreach (var myString in Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                csv.AppendLine(myString);
            }

            File.WriteAllText("report.csv", csv.ToString());
        }
    }
}
