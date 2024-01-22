using Microsoft.EntityFrameworkCore;
using MYRTEX.Domain.Models;

namespace MYRTEX.Infrastructure
{
    /// <summary>
    /// Контекст базы данных приложения для работы с данными сотрудников.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="options">Настройки контекста базы данных.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Получает или задает набор данных для сущностей сотрудников.
        /// </summary>
        public DbSet<EmployeeEntity> Employees { get; set; } = null!;
    }
}
