using System;

namespace MYRTEX.Domain.Exceptions
{
    /// <summary>
    /// »сключение, выбрасываемое в случае отсутстви€ сущности.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="EntityNotFoundException"/>.
        /// </summary>
        /// <param name="message">—ообщение об ошибке.</param>
        public EntityNotFoundException(string message = "Entity not found") : base(message)
        {
        }
    }
}
