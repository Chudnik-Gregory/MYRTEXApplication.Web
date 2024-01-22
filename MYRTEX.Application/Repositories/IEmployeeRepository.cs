using MYRTEX.Application.Commands;
using MYRTEX.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYRTEX.Application.Repositories
{
    /// <summary>
    /// ��������� ����������� ��� ������ � ������� �����������.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// �������� ��� ������������ ������ � �����������.
        /// </summary>
        /// <returns>��������� ��������� �����������.</returns>
        Task<IReadOnlyCollection<EmployeeEntity>> GetAll();

        /// <summary>
        /// ������� ����� �������� ����������.
        /// </summary>
        /// <param name="employee">�������� ���������� ��� ��������.</param>
        /// <returns>��������� �������� ����������.</returns>
        Task<EmployeeEntity> Create(EmployeeEntity employee);

        /// <summary>
        /// ��������� ���������� � ���������� �� ������ ��������������� �������.
        /// </summary>
        /// <param name="employee">������� ��� ���������� ���������� � ����������.</param>
        /// <returns>����������� �������� ����������.</returns>
        Task<EmployeeEntity> Update(PutEmployeeCommand employee);

        /// <summary>
        /// ������� �������� ���������� �� ��������������.
        /// </summary>
        /// <param name="id">������������� ����������.</param>
        Task Delete(long id);
    }
}
