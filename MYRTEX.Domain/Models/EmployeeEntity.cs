using System;
using System.ComponentModel.DataAnnotations;

namespace MYRTEX.Domain.Models
{
    public class EmployeeEntity : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string Department { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? EmploymentDate { get; set; }
        [Required]
        public decimal Salary { get; set; }


        // Вычисляемое свойство для полного имени
        public string FullName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(MiddleName))
                {
                    return $"{FirstName} {MiddleName} {LastName}";
                }
                else
                {
                    return $"{FirstName} {LastName}";
                }
            }
        }
    }
}
