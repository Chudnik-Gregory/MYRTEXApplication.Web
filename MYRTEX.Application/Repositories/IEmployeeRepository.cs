using MYRTEX.Application.Commands;
using MYRTEX.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYRTEX.Application.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с данными сотрудников.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Получает все существующие записи о сотрудниках.
        /// </summary>
        /// <returns>Коллекция сущностей сотрудников.</returns>
        Task<IReadOnlyCollection<EmployeeEntity>> GetAll();

        /// <summary>
        /// Создает новую сущность сотрудника.
        /// </summary>
        /// <param name="employee">Сущность сотрудника для создания.</param>
        /// <returns>Созданная сущность сотрудника.</returns>
        Task<EmployeeEntity> Create(EmployeeEntity employee);

        /// <summary>
        /// Обновляет информацию о сотруднике на основе предоставленной команды.
        /// </summary>
        /// <param name="employee">Команда для обновления информации о сотруднике.</param>
        /// <returns>Обновленная сущность сотрудника.</returns>
        Task<EmployeeEntity> Update(PutEmployeeCommand employee);

        /// <summary>
        /// Удаляет сущность сотрудника по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        Task Delete(long id);
    }
}
