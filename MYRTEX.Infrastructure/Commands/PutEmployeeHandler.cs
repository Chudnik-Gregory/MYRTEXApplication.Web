using MediatR;
using MYRTEX.Application.Commands;
using MYRTEX.Application.Repositories;
using MYRTEX.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MYRTEX.Infrastructure.Commands
{
    /// <summary>
    /// Обработчик команды обновления информации о сотруднике.
    /// </summary>
    public class PutEmployeeHandler : IRequestHandler<PutEmployeeCommand, EmployeeEntity>
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PutEmployeeHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Репозиторий сотрудников.</param>
        public PutEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Обрабатывает команду обновления информации о сотруднике.
        /// </summary>
        /// <param name="request">Команда обновления информации о сотруднике.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Обновленная сущность сотрудника.</returns>
        public async Task<EmployeeEntity> Handle(PutEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.Update(request);
        }
    }
}
