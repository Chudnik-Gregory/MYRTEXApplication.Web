using MediatR;
using MYRTEX.Application.Commands;
using MYRTEX.Application.Repositories;
using System.Threading.Tasks;
using System.Threading;

namespace MYRTEX.Infrastructure.Commands
{
    /// <summary>
    /// Обработчик команды удаления сотрудника.
    /// </summary>
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteEmployeeHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Репозиторий сотрудников.</param>
        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Обрабатывает команду удаления сотрудника.
        /// </summary>
        /// <param name="request">Команда удаления сотрудника.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Завершенная задача.</returns>
        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.Delete(request.Id);
            return Unit.Value;
        }
    }
}
