using MYRTEX.Application.Queries;
using MYRTEX.Application.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using System.Linq;

namespace MYRTEX.Infrastructure.Queries
{
    /// <summary>
    /// Обработчик запроса для получения списка сотрудников на основе указанного запроса.
    /// </summary>
    public class GetEmployees : IRequestHandler<GetEmployeesQuery, List<GetEmployeeResult>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetEmployees"/>.
        /// </summary>
        /// <param name="employeeRepository">Реализация репозитория сотрудников.</param>
        public GetEmployees(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение списка сотрудников.
        /// </summary>
        /// <param name="query">Запрос на получение списка сотрудников.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Список результатов, представляющих сотрудников.</returns>
        public async Task<List<GetEmployeeResult>> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAll();

            var employeeResults = employees.Select(item => new GetEmployeeResult
            {
                Id = item.Id,
                FIO = item.FullName,
                BirthDate = item.BirthDate,
                Department = item.Department,
                EmploymentDate = item.EmploymentDate,
                Salary = item.Salary,
            }).ToList();

            return employeeResults;
        }
    }
}
