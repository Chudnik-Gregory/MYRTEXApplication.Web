using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MYRTEX.Application.Commands;
using MYRTEX.Application.Repositories;
using MYRTEX.Domain.Exceptions;
using MYRTEX.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MYRTEX.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с данными сотрудников в БД.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EmployeeRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        /// <param name="logger">Логгер для записи событий.</param>
        public EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<EmployeeEntity>> GetAll() =>
            await _context.Employees.ToListAsync();

        /// <inheritdoc />
        public async Task<EmployeeEntity> Create(EmployeeEntity employee)
        {
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        /// <inheritdoc />
        public async Task<EmployeeEntity> Update(PutEmployeeCommand employee)
        {
            var employeeEntity = await _context.Employees.FindAsync(employee.Id);
            if (employeeEntity is null)
                throw new EntityNotFoundException();


            employeeEntity.FirstName = UpdateField(employee.FirstName, employeeEntity.FirstName, nameof(employeeEntity.FirstName));
            employeeEntity.LastName = UpdateField(employee.LastName, employeeEntity.LastName, nameof(employeeEntity.LastName));
            employeeEntity.MiddleName = UpdateField(employee.MiddleName, employeeEntity.MiddleName, nameof(employeeEntity.MiddleName));
            employeeEntity.Department = UpdateField(employee.Department, employeeEntity.Department, nameof(employeeEntity.Department));
            if (employee.EmploymentDate.HasValue)
            {
                employeeEntity.EmploymentDate = 
                    UpdateField(employee.EmploymentDate, employeeEntity.EmploymentDate, nameof(employeeEntity.EmploymentDate)).Value;
            }
            if (employee.DateOfBirth.HasValue)
            {
                employeeEntity.BirthDate = 
                    UpdateField(employee.DateOfBirth, employeeEntity.BirthDate, nameof(employeeEntity.BirthDate)).Value;
            }
            if (employee.Salary.HasValue)
            {
                employeeEntity.Salary = UpdateField(employee.Salary, employeeEntity.Salary, nameof(employeeEntity.Salary)).Value;
            }

            employeeEntity.UpdatedAt = DateTime.UtcNow;
            var result = _context.Update(employeeEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        /// <inheritdoc />
        public async Task Delete(long id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
                throw new EntityNotFoundException();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет значение поля с учетом старого и нового значения, логируя изменения.
        /// </summary>
        /// <typeparam name="T">Тип значения поля.</typeparam>
        /// <param name="newValue">Новое значение поля.</param>
        /// <param name="currentValue">Текущее значение поля.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Обновленное значение поля.</returns>
        private T UpdateField<T>(T newValue, T currentValue, string fieldName)
        {
            if (!EqualityComparer<T>.Default.Equals(newValue, currentValue))
            {
                _logger.LogInformation($"{fieldName} has changed");
                return newValue;
            }

            return currentValue;
        }
    }
}
