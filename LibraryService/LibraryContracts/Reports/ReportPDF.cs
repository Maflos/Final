namespace LibraryContracts.Reports
{
    public class ReportPDF : Report
    {
        public override DocumentGeneral CreateReport()
        {
            return new DocumentPDF();
        }
    }
}
