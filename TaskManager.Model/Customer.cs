using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required..")]
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }

        public string SPOCName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Type { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
