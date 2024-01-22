using System;

namespace MYRTEX.Application.Queries
{
    /// <summary>
    /// Результат запроса для получения информации о сотруднике.
    /// </summary>
    public class GetEmployeeResult
    {
        public long Id { get; set; }
        public string FIO { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Department { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public decimal Salary { get; set; }
    }
}