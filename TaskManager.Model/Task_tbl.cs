using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class Task_tbl
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Task Name is required.")]
        [Display(Name = "Task")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        [Display(Name = "Priority")]
        public int PriorityID { get; set; }
        
        [Display(Name = "Estimated Time")]
        public TimeSpan EstTime { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        [Display(Name = "Employee")]
        public int EmployeeID { get; set; }
        /*public virtual int EmployeeID { get; set; }*/
        /*public virtual Department Departments { get; set; }*/

        public int CreatedBy { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
