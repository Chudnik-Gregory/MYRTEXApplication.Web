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
        public string FIO { get; set; }
        public string Department { get; set; } = null!;
        public DateTime BirthDate { get; set; } = default!;
        public DateTime EmploymentDate { get; set; } = default!;
        public decimal Salary { get; set; } = 0;
    }
}
