using Domain.Entities;
using Infrastructure.Common.Interfaces;
using Infrastructure.Common.Models;
using Infrastructure.DataAccess.FilterOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.IRepositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<PaginatedList<Student>> FindAllStudentsAsync(StudentFilterOptions? options = null, int pageNumber = 0, int pageSize = int.MaxValue);
    }
}
