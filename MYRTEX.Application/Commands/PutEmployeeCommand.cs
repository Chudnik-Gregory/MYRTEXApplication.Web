using MediatR;
using MYRTEX.Domain.Models;
using System;

namespace MYRTEX.Application.Commands
{
    /// <summary>
    /// Команда для обновления информации о сотруднике.
    /// </summary>
    public class PutEmployeeCommand : IRequest<EmployeeEntity>
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Department { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public decimal? Salary { get; set; }
    }
}
