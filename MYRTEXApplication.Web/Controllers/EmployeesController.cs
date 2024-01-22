using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MYRTEX.Application.Commands;
using MYRTEX.Application.Queries;
using MYRTEX.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace MYRTEX.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с данными сотрудников.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeesController> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EmployeesController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр <see cref="IMediator"/>, предоставляющий медиатор для отправки команд и запросов.</param>
        /// <param name="logger">Экземпляр <see cref="ILogger{TCategoryName}"/> для записи логов.</param>
        public EmployeesController(IMediator mediator, ILogger<EmployeesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Получает список сотрудников.
        /// </summary>
        /// <returns>Список сотрудников.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _mediator.Send(new GetEmployeesQuery());
            return Ok(employees);
        }

        /// <summary>
        /// Добавляет нового сотрудника.
        /// </summary>
        /// <param name="command">Команда для добавления сотрудника.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddEmployeeCommand command)
        {
            try
            {
                var employee = await _mediator.Send(command);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Add");
                return BadRequest("Employee ");
            }
        }

        /// <summary>
        /// Обновляет данные сотрудника.
        /// </summary>
        /// <param name="command">Команда для обновления данных сотрудника.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody] AddEmployeeCommand command)
        {
            try
            {
                var employee = await _mediator.Send(command);
                return Ok(employee);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message, "Put");
                return BadRequest("Not found in database");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Put");
                return BadRequest();
            }

        }

        /// <summary>
        /// Удаляет сотрудника по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <returns>Результат выполнения операции.</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!long.TryParse(id, out var employeeId))
            {
                return BadRequest();
            }
            var command = new DeleteEmployeeCommand { Id = employeeId };
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message, "Delete");
                return BadRequest("Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Delete");
                return BadRequest();
            }
        }
    }
}
