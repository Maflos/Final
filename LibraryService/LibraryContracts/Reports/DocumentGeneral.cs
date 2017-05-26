namespace LibraryContracts.Reports
{
    public abstract class DocumentGeneral
    {
        public string Text { get; set; }

        public abstract void GenerateReport();
    }
}
