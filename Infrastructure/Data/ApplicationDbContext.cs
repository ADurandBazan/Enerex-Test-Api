using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    /// <summary>
    /// ApplicationDbContext class for managing the database context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext class
        /// </summary>
        public ApplicationDbContext() { }

        /// <summary>
        /// Configures the database context options
        /// </summary>
        /// <param name="options">Database context options builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("StudentsDb");
            options.UseLazyLoadingProxies(true);
        }

        /// <summary>
        /// Configures the database model
        /// </summary>
        /// <param name="builder">Database model builder</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        /// <summary>
        /// DbSet for the Student entity
        /// </summary>
        public DbSet<Student> Students { get; set; }
    }
}
