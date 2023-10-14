using Demo.DAl.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Welcome.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(50, ErrorMessage = "max length is 50 char")]
        [MinLength(5, ErrorMessage = "min length is 5 char")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public IFormFile Image{ get; set; }
        public string? ImageName{ get; set; }
        public DateTime HireDate { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

    }
}
