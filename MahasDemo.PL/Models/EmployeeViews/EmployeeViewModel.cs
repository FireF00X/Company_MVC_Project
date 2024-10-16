using MahasDemo.DAL.Data.Model;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MahasDemo.PL.Models.EmployeeViews
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }
    public class EmployeeViewModel 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 20 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is a required field.")]
        [Range(21, 60, ErrorMessage = "Age must be between 21 and 60.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Salary is a required field.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Gender is a required field.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is a required field.")]
        [RegularExpression(@"^\d{1,5}-[a-zA-Z\s]{2,50}-[a-zA-Z\s]{2,50}-[a-zA-Z\s]{2,50}$",
            ErrorMessage = "Address must be in the format: 123-Street-City-Country.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone Number is a required field.")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageName {  get; set; }
    }
}
