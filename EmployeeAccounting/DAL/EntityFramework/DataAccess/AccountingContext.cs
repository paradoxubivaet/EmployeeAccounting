using EmployeeAccounting.DAL.EntityFramework.Model;
using System.Data.Entity;

namespace EmployeeAccounting.DAL.EntityFramework.DataAccess
{
    public class AccountingContext : DbContext
    {
        public AccountingContext() : base("AccountingDBconnectionString")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<EmployeeModel> EmployeeAccounts { get; set; }
    }
}
