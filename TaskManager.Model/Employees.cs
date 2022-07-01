using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Department is required..")]
        [Display(Name = "Department Name")]
        public virtual int DepartmentID { get; set; }
        public virtual Department Departments { get; set; }

        public string Reportingto { get; set; }
        public string Role { get; set; }
        //public virtual int FunctionID { get; set; }
        //public virtual Function Functions { get; set; }

        public string Function { get; set; }
        public string Team { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
