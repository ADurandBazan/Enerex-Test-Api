using Domain.Entities;
using Infrastructure.Common.Interfaces;
using Infrastructure.Common.Models;
using Infrastructure.DataAccess.FilterOptions;

namespace Infrastructure.DataAccess.IRepositories
{
    /// <summary>
    /// IStudentRepository interface for working with Student entities
    /// </summary>
    public interface IStudentRepository : IRepository<Student>
    {
        /// <summary>
        /// Finds all students asynchronously with pagination and filtering options
        /// </summary>
        /// <param name="options">The filtering options for the students</param>
        /// <param name="pageNumber">The page number to retrieve</param>
        /// <param name="pageSize">The number of students to retrieve per page</param>
        /// <returns>A PaginatedList of Students</returns>
        Task<PaginatedList<Student>> FindAllStudentsAsync(StudentFilterOptions? options = null, int pageNumber = 0, int pageSize = int.MaxValue);
    }
}
