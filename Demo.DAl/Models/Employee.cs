using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public int? Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary{ get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string ImageName { get; set; }
        public DateTime HireCreation { get; set; }= DateTime.Now;

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department? Department { get; set; }






    }
}
