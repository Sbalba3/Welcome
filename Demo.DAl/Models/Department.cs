using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Required !")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required !")]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Employee> Employees = new HashSet<Employee>();


    }
}
