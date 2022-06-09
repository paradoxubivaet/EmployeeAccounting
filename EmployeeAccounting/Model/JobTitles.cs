using System.Collections.Generic;

namespace EmployeeAccounting.DAL.EntityFramework.Model
{
    public class JobTitles
    {
        public static string Director { get; } = "Директор"; 
        public static string DepartamentHead { get; } = "Руководитель подразделения"; 
        public static string Controller { get; } = "Контролер"; 
        public static string Worker { get; } = "Рабочий"; 

        public static List<string> GetJobTitles()
        {
            List<string> jobTitles = new List<string>();

            jobTitles.Add(Director);
            jobTitles.Add(DepartamentHead);
            jobTitles.Add(Controller);
            jobTitles.Add(Worker);
            jobTitles.Add("");

            return jobTitles;
        }
    }
}
