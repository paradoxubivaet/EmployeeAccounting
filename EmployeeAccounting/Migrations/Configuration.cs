namespace EmployeeAccounting.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeAccounting.DAL.EntityFramework.DataAccess.AccountingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EmployeeAccounting.DAL.EntityFramework.DataAccess.AccountingContext";
        }

        protected override void Seed(EmployeeAccounting.DAL.EntityFramework.DataAccess.AccountingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
