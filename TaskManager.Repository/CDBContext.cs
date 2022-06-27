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
        public CDBContext():base("TaskManager")
        {

        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Function> Functions { get; set; }

       
    }
}
