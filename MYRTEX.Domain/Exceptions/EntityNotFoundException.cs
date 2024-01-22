using System;

namespace MYRTEX.Domain.Exceptions
{
    /// <summary>
    /// ����������, ������������� � ������ ���������� ��������.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="EntityNotFoundException"/>.
        /// </summary>
        /// <param name="message">��������� �� ������.</param>
        public EntityNotFoundException(string message = "Entity not found") : base(message)
        {
        }
    }
}
