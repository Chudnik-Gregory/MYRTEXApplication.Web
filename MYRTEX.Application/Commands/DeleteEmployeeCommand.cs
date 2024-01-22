using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYRTEX.Application.Commands
{
    /// <summary>
    /// Команда для удаления сотрудника.
    /// </summary>
    public class DeleteEmployeeCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
