using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAccounting.DAL.EntityFramework.Model
{
    public class EmployeeModel
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
    }
}
