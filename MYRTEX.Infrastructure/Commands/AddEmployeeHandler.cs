using MediatR;
using MYRTEX.Domain.Models;
using MYRTEX.Application.Commands;
using System.Threading;
using System.Threading.Tasks;
using MYRTEX.Application.Repositories;
using System.Text.RegularExpressions;

namespace MYRTEX.Infrastructure.Commands
{
    /// <summary>
    /// Обработчик команды добавления нового сотрудника.
    /// </summary>
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, EmployeeEntity>
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddEmployeeHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Репозиторий сотрудников.</param>
        public AddEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Обрабатывает команду добавления нового сотрудника.
        /// </summary>
        /// <param name="request">Команда добавления нового сотрудника.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Сущность нового сотрудника.</returns>
        public async Task<EmployeeEntity> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            (string firstName, string lastName, string middleName) = ParseFullName(request.FIO);

            return await _employeeRepository.Create(new EmployeeEntity
            {
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                BirthDate = request.BirthDate,
                EmploymentDate = request.EmploymentDate,
                Department = request.Department,
                Salary = request.Salary,
            });
        }

        private (string firstName, string lastName, string middleName) ParseFullName(string fullName)
        {
            // Регулярное выражение для разбора ФИО (или ФИ)
            Regex regex = new Regex(@"\b(\w+)\s+(\w+)(?:\s+(\w+))?\b");
            // Поиск соответствия в строке
            Match match = regex.Match(fullName);

            if (match.Success)
            {
                return (match.Groups[2].Value, match.Groups[1].Value, match.Groups[3].Value);
            }

            return (null, null, null);
        }
    }
}
