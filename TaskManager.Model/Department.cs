using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required..")]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
