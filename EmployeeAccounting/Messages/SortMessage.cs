namespace EmployeeAccounting.Messages
{
    public class SortMessage
    {
        public string Division { get; }
        public string JobTitle { get; }

        public SortMessage(string division, string jobTitle)
        {
            Division = division;
            JobTitle = jobTitle;
        }
    }
}
