namespace LibraryContracts.Reports
{
    public class ReportCSV : Report
    {
        public override DocumentGeneral CreateReport()
        {
            return new DocumentCSV();
        }
    }
}
