using MediatR;
using MYRTEX.Domain.Models;
using MYRTEX.Application.Commands;
using System.Threading;
using System.Threading.Tasks;
using MYRTEX.Application.Repositories;

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
            return await _employeeRepository.Create(new EmployeeEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                BirthDate = request.DateOfBirth,
                EmploymentDate = request.EmploymentDate,
                Department = request.Department,
                Salary = request.Salary,
            });
        }
    }
}
