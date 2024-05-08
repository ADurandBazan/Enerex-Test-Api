using Domain.Entities;
using Infrastructure.Common.Models;
using Infrastructure.Common.Utilities;
using Infrastructure.Data;
using Infrastructure.DataAccess.FilterOptions;
using Infrastructure.DataAccess.IRepositories;

namespace Infrastructure.DataAccess.Repositories
{
    /// <summary>
    /// StudentRepository class for working with Student entities
    /// </summary>
    /// <seealso cref="Repository{Student}" />
    /// <seealso cref="IStudentRepository" />
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentRepository"/> class
        /// </summary>
        /// <param name="context">The context to use for database operations</param>
        public StudentRepository(IUnitOfWork<ApplicationDbContext> context) : base(context)
        {
        }

        /// <summary>
        /// Finds all students asynchronously with pagination and filtering options
        /// </summary>
        /// <param name="options">The filtering options for the students</param>
        /// <param name="pageNumber">The page number to retrieve</param>
        /// <param name="pageSize">The number of students to retrieve per page</param>
        /// <returns>A PaginatedList of Students</returns>
        public async Task<PaginatedList<Student>> FindAllStudentsAsync(StudentFilterOptions? options = null, int pageNumber = 0, int pageSize = int.MaxValue)
        {
            IQueryable<Student> query = Entity.AsQueryable();

            if (options is not null)
            {
                if (!string.IsNullOrEmpty(options.Name))
                {
                    query = query.Where(e => e.Name.ToLower().Contains(options.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(options.Education))
                {
                    query = query.Where(e => e.Education.ToLower().Contains(options.Education.ToLower()));
                }
                if (options.Age > 0)
                {
                    query = query.Where(e => e.Age == options.Age);
                }
                if (options.AcademicYear > 0)
                {
                    query = query.Where(e => e.AcademicYear == options.AcademicYear);
                }
                if (!string.IsNullOrEmpty(options.Gender))
                {
                    switch (options.Gender.ToLower())
                    {
                        case "f":
                            query = query.Where(e => e.Gender == Domain.Enums.Gender.F);
                            break;
                        case "m":
                            query = query.Where(e => e.Gender == Domain.Enums.Gender.M);
                            break;
                        default:
                            throw new ArgumentException($"Invalid value for Gender: {options.Gender}");
                    }
                }

                if (options.IsDescending)
                    query = query.OrderByDescending(e => e.Id);
                else
                    query = query.OrderBy(e => e.Id);
            }

            return await query.PaginatedListAsync(pageNumber, pageSize);
        }
    }
}
