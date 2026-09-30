using System.ComponentModel.DataAnnotations;
namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateofJoining { get; set; }
        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        [Range(1, double.MaxValue)]
        public decimal Salary { get; set; }
    }
}
 
