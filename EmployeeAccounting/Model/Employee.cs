using EmployeeAccounting.DAL.EntityFramework.Model;
using System;

namespace EmployeeAccounting.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirth { get; set; }
        public string Gender { get; set; }
        public string JobTitle { get; set; }
        public string? SubdivisionName { get; set; }
        public string? DepartamentHeadName { get; set; }

        public Employee() { }

        public Employee(int id,
                        string firstName,
                        string secondName, 
                        string patronymic,
                        DateTime dateBirth,
                        string gender,
                        string jobTitle, 
                        string? subdivisionName, 
                        string? departamendHeadName)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
            Patronymic = patronymic;
            DateBirth = dateBirth;
            Gender = gender;
            JobTitle = jobTitle;
            SubdivisionName = subdivisionName;
            DepartamentHeadName = departamendHeadName;
        }

        public void Promote()
        {
            if (JobTitle == JobTitles.Director)
                return;
            else if (JobTitle == JobTitles.DepartamentHead)
                JobTitle = JobTitles.Director;
            else if (JobTitle == JobTitles.Controller)
                JobTitle = JobTitles.DepartamentHead;
            else if (JobTitle == JobTitles.Worker)
                JobTitle = JobTitles.Controller;
            else if (JobTitle == "")
                JobTitle = JobTitles.Worker;
        }

        public void Demote()
        {
            if (JobTitle == JobTitles.Worker)
                return;
            else if (JobTitle == "")
                return;
            else if (JobTitle == JobTitles.Director)
                JobTitle = JobTitles.DepartamentHead;
            else if (JobTitle == JobTitles.DepartamentHead)
                JobTitle = JobTitles.Controller;
            else if (JobTitle == JobTitles.Controller)
                JobTitle = JobTitles.Worker;
        }
    }
}
