using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TaskManager.Model;

namespace TaskManager.Repository
{
    public class CDBContext:DbContext
    {
        
        public CDBContext():base("Data Source=tcp:myomnex.database.windows.net,1433;Initial Catalog=OmnexEmployeeManagement;Persist Security Info=False;User ID=OmLoginUser;Password=Test@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CDBContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Function> Functions { get; set; }
        public DbSet<Task_tbl> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employees> Employee { get; set; }



    }
}
