using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace LibraryContracts.Reports
{
    public class DocumentCSV : DocumentGeneral
    {
        public override void GenerateReport()
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("report.pdf", FileMode.Create));
            doc.Open();

            Paragraph paragraph = new Paragraph(Text);
            doc.Add(paragraph);
            doc.Close();
        }
    }
}
