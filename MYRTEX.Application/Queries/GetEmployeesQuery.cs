using MediatR;
using System;
using System.Collections.Generic;

namespace MYRTEX.Application.Queries
{
    /// <summary>
    /// Запрос для получения списка сотрудников.
    /// </summary>
    public class GetEmployeesQuery : IRequest<List<GetEmployeeResult>>
    {
    }
}
