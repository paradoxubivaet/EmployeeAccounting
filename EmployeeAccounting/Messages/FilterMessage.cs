using System.Collections.Generic;

namespace EmployeeAccounting.Messages
{
    public class FilterMessage
    {
        public List<string>? Divisions { get; }
        public List<string>? JobTitles { get; }

        public FilterMessage(List<string> divisions, List<string> jobTitles)
        {
            Divisions = divisions;
            JobTitles = jobTitles;
        }
    }
}
