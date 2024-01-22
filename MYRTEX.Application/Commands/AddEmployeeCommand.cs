using MediatR;
using MYRTEX.Domain.Models;
using System;

namespace MYRTEX.Application.Commands
{
    /// <summary>
    /// Команда для добавления сотрудника.
    /// </summary>
    public class AddEmployeeCommand : IRequest<EmployeeEntity>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public DateTime DateOfBirth { get; set; } = default!;
        public DateTime EmploymentDate { get; set; } = default!;
        public decimal Salary { get; set; } = 0;
    }
}
