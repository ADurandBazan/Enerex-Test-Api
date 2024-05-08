using Application.ApplicationServices.Maps;
using Application.Common.Interfaces;
using Infrastructure.DataAccess.FilterOptions;

namespace Application.ApplicationServices.IServices
{
    /// <summary>
    /// Interface for student service
    /// </summary>
    public interface IStudentService : ICrudService<StudentDto>
    {
        /// <summary>
        /// Finds all students asynchronously based on the provided filter options and pagination settings
        /// </summary>
        /// <param name="filterOptions">Optional filter options to apply to the search</param>
        /// <param name="pageNumber">Optional page number to retrieve (defaults to 1)</param>
        /// <param name="pageSize">Optional page size to retrieve (defaults to int.MaxValue)</param>
        /// <returns>A task that represents the asynchronous operation, containing a list of student DTOs</returns>
        Task<IEnumerable<StudentDto>> FindAllStudentsAsync(StudentFilterOptions? filterOptions = null, int pageNumber = 1, int pageSize = int.MaxValue);
    }
}
